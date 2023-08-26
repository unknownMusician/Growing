using System;
using System.Collections.Generic;
using System.Linq;
using AreYouFruits.Assertions;
using AreYouFruits.ConstructorGeneration;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Growing.PlanetGeneration
{
    public partial struct Triangle
    {
        [GenerateConstructor] public Vector3 Vertex0;
        [GenerateConstructor] public Vector3 Vertex1;
        [GenerateConstructor] public Vector3 Vertex2;
    }
    
    public sealed partial class PlanetGenerator
    {
        [GenerateConstructor] private readonly PlanetGenerationSettings planetGenerationSettings;
        [GenerateConstructor] private readonly IIcosahedronDataProvider icosahedronDataProvider;
        [GenerateConstructor] private readonly PlanetHolder planetHolder;
        
        public void Generate()
        {
            _ = planetGenerationSettings.Detailing;

            GameObject planetObject = Object.Instantiate(planetGenerationSettings.Prefab);

            planetObject.GetComponent<MeshFilter>().sharedMesh = GenerateIcoSphereMesh();
            
            planetHolder.Value = planetObject;
        }

        private Mesh GenerateIcoSphereMesh()
        {
            return GenerateMesh();
        }

        private Mesh GenerateMesh()
        {
            var (vertices, triangles) = SubdivideTriangles(icosahedronDataProvider.Vertices, icosahedronDataProvider.Triangles);
            
            var mesh = new Mesh
            {
                name = "Generated Planet",
                vertices = vertices,
                triangles = triangles,
            };
            
            mesh.RecalculateNormals();

            return mesh;
        }

        private (Vector3[], int[]) SubdivideTriangles(Vector3[] vertices, int[] triangles)
        {
            var resultVertices = new List<Vector3>();

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Triangle[] subdivideTriangles = SubdivideTriangle(new Triangle
                {
                    Vertex0 = vertices[triangles[i + 0]],
                    Vertex1 = vertices[triangles[i + 1]],
                    Vertex2 = vertices[triangles[i + 2]],
                });

                foreach (Triangle subdivideTriangle in subdivideTriangles)
                {
                    resultVertices.Add(subdivideTriangle.Vertex0);
                    resultVertices.Add(subdivideTriangle.Vertex1);
                    resultVertices.Add(subdivideTriangle.Vertex2);
                }
            }

            return (resultVertices.ToArray(), resultVertices.Select((_, i) => i).ToArray());
        }

        private Triangle[] SubdivideTriangle(Triangle triangle)
        {
            int detailing = planetGenerationSettings.Detailing;

            if (detailing < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(detailing), detailing, null);
            }

            Vector3[] triangleVertices = SubdivideTriangleVertices(triangle, detailing);

            return AssembleSubdividedTriangles(triangleVertices, detailing);
        }

        private static Vector3[] SubdivideTriangleVertices(Triangle triangle, int detailing)
        {
            var results = new List<Vector3>();

            for (int row = 0; row <= detailing; row++)
            {
                Vector3 rowStart = Vector3.Lerp(triangle.Vertex0, triangle.Vertex1, (float)row / detailing);
                Vector3 rowEnd = Vector3.Lerp(triangle.Vertex0, triangle.Vertex2, (float)row / detailing);

                for (int column = 0; column <= row; column++)
                {
                    Vector3 vertex = (row == 0) switch
                    {
                        true => triangle.Vertex0,
                        false => Vector3.Lerp(rowStart, rowEnd, (float)column / row),
                    };

                    results.Add(vertex);
                }
            }
            
            return results.ToArray();
        }

        // todo
        // private Triangle[] AssembleSubdividedTriangles1(Vector3[] vertices)
        // {
        //     Vector3[] i = vertices;
        //     
        //     var results = new List<Triangle>();
        //
        //     int row;
        //     int rowFirstIndex;
        //     int column;
        //     int columnsCount;
        //
        //     row = 0;
        //     rowFirstIndex = 0;
        //     column = 0;
        //     columnsCount = row * 2 + 1;
        //     results.Add(new Triangle(i[rowFirstIndex], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));
        //     
        //     row = 1;
        //     rowFirstIndex = 1;
        //     columnsCount = row * 2 + 1;
        //     column = 0;
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + column + 1], i[rowFirstIndex + row + column + 2]));
        //     column = 1;
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));
        //     
        //     row = 2;
        //     rowFirstIndex = 3;
        //     columnsCount = row * 2 + 1;
        //     column = 0;
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + column + 1], i[rowFirstIndex + row + column + 2]));
        //     column = 1;
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + column + 1], i[rowFirstIndex + row + column + 2]));
        //     column = 2;
        //     results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));
        //     
        //     return results.ToArray();
        // }

        private Triangle[] AssembleSubdividedTriangles(Vector3[] vertices, int detailing)
        {
            Vector3[] i = vertices;
            
            var results = new List<Triangle>();

            int rowFirstIndex = 0;

            for (int row = 0; row < detailing; row++)
            {
                rowFirstIndex += row;

                int columnsCount = row + 1;

                for (int column = 0; column < columnsCount; column++)
                {
                    results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 1], i[rowFirstIndex + row + column + 2]));

                    if (column != columnsCount - 1)
                    {
                        results.Add(new Triangle(i[rowFirstIndex + column], i[rowFirstIndex + row + column + 2], i[rowFirstIndex + column + 1]));
                    }
                }
            }

            return results.ToArray();
        }
    }
}
