﻿using System;
using System.Linq;
using AreYouFruits.ConstructorGeneration;
using AreYouFruits.MonoBehaviourUtils.Unity;
using Growing.Settings;
using Growing.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Growing.PlanetGeneration
{
    public sealed partial class PlanetGenerator
    {
        [GenerateConstructor] private readonly PlanetGenerationSettings planetGenerationSettings;
        [GenerateConstructor] private readonly IIcosahedronDataProvider icosahedronDataProvider;
        [GenerateConstructor] private readonly PlanetHolder planetHolder;

        private Vector3[] singleSubdividedTriangleVerticesBuffer;
        
        public void Generate()
        {
            if (!planetHolder.Value.TryGet(out var planetObject))
            {
                planetObject = Object.Instantiate(planetGenerationSettings.Prefab);
                planetHolder.Value = planetObject;
            }

            var mesh = GenerateMesh();
            
            planetObject.GetComponentOrThrow<MeshFilter>().sharedMesh = mesh;
            planetObject.GetComponentOrThrow<MeshCollider>().sharedMesh = mesh;
        }

        private Mesh GenerateMesh()
        {
            singleSubdividedTriangleVerticesBuffer = new Vector3[GetSubdividedTriangleVerticesCount(planetGenerationSettings.Detailing)];
            
            var (vertices, triangles) = SubdivideTriangles(
                icosahedronDataProvider.Vertices.Select(vertex => vertex * planetGenerationSettings.Radius).ToArray(),
                icosahedronDataProvider.Triangles);
            
            var mesh = new Mesh
            {
                name = "Generated Planet",
                vertices = vertices,
                triangles = triangles,
            };
            
            mesh.RecalculateNormals();

            singleSubdividedTriangleVerticesBuffer = null;

            return mesh;
        }

        private (Vector3[], int[]) SubdivideTriangles(Vector3[] vertices, int[] triangles)
        {
            var subdividedTrianglesCount = GetSubdividedTrianglesCount(planetGenerationSettings.Detailing);

            var resultTrianglesCount = triangles.Length * subdividedTrianglesCount;

            var resultVertices = new Vector3[resultTrianglesCount];

            var subdividedTriangles = new Triangle[subdividedTrianglesCount];

            for (var i = 0; i < triangles.Length / 3; i++)
            {
                SubdivideTriangle(new Triangle
                {
                    Vertex0 = vertices[triangles[i * 3 + 0]],
                    Vertex1 = vertices[triangles[i * 3 + 1]],
                    Vertex2 = vertices[triangles[i * 3 + 2]],
                }, subdividedTriangles);

                for (var j = 0; j < subdividedTriangles.Length; j++)
                {
                    var subdividedTriangle = subdividedTriangles[j];

                    resultVertices[i * subdividedTrianglesCount * 3 + j * 3 + 0] = subdividedTriangle.Vertex0;
                    resultVertices[i * subdividedTrianglesCount * 3 + j * 3 + 1] = subdividedTriangle.Vertex1;
                    resultVertices[i * subdividedTrianglesCount * 3 + j * 3 + 2] = subdividedTriangle.Vertex2;
                }
            }

            return (resultVertices, NumerateLowPolyTriangles(resultVertices));
        }

        private static int[] NumerateLowPolyTriangles(Vector3[] vertices)
        {
            var results = new int[vertices.Length];

            for (var i = 0; i < results.Length; i++)
            {
                results[i] = i;
            }
            
            return results;
        }

        private void SubdivideTriangle(Triangle triangle, Span<Triangle> results)
        {
            var detailing = planetGenerationSettings.Detailing;

            if (detailing < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(detailing), detailing, null);
            }

            var triangleVertices = SubdivideTriangleVertices(triangle, detailing);

            AssembleSubdividedTriangles(triangleVertices, detailing, results);
        }

        private Vector3[] SubdivideTriangleVertices(Triangle triangle, int detailing)
        {
            var results = singleSubdividedTriangleVerticesBuffer;
            var resultsFiller = new SpanFiller<Vector3>(results);

            for (var row = 0; row <= detailing; row++)
            {
                var horizontalT = (float)row / detailing;
                
                var rowStart = Vector3.SlerpUnclamped(triangle.Vertex0, triangle.Vertex1, horizontalT);
                var rowEnd = Vector3.SlerpUnclamped(triangle.Vertex0, triangle.Vertex2, horizontalT);

                for (var column = 0; column <= row; column++)
                {
                    var vertex = (row == 0) switch
                    {
                        true => triangle.Vertex0,
                        false => Vector3.SlerpUnclamped(rowStart, rowEnd, (float)column / row),
                    };

                    resultsFiller.Add(vertex);
                }
            }
            
            return results;
        }

        private static void AssembleSubdividedTriangles(Vector3[] vertices, int detailing, Span<Triangle> results)
        {
            var resultsFiller = new SpanFiller<Triangle>(results);

            var rowFirstIndex = 0;

            for (var row = 0; row < detailing; row++)
            {
                rowFirstIndex += row;

                var columnsCount = row + 1;

                for (var column = 0; column < columnsCount; column++)
                {
                    var topVertex = vertices[rowFirstIndex + column];
                    var leftBottomVertex = vertices[rowFirstIndex + column + row + 2];
                    var rightBottomVertex = vertices[rowFirstIndex + column + row + 1];
                    
                    resultsFiller.Add(new Triangle(topVertex, rightBottomVertex, leftBottomVertex));

                    if (column != columnsCount - 1)
                    {
                        var rightTopVertex = vertices[rowFirstIndex + column + 1];
                        resultsFiller.Add(new Triangle(topVertex, leftBottomVertex, rightTopVertex));
                    }
                }
            }
        }

        private static int GetSubdividedTrianglesCount(int detailing)
        {
            return detailing * detailing;
        }

        private static int GetSubdividedTriangleVerticesCount(int detailing)
        {
            return (detailing + 1) * (detailing + 2) / 2;
        }
    }
}
