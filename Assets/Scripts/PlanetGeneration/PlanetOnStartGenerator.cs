using System;
using AreYouFruits.InitializerGeneration;
using UnityEngine;

namespace Growing.PlanetGeneration
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class PlanetOnStartGenerator : MonoBehaviour
    {
        [GenerateInitializer] private PlanetGenerator planetGenerator;
        // todo
        // [GenerateInitializer] private PlanetHolder planetHolder;
        //
        // [SerializeField] private float showMaxDistance;
        // [SerializeField] private Vector3 showCenter;
        
        private void Update()
        {
            planetGenerator.Generate();
        }

        // todo
        // private void OnDrawGizmos()
        // {
        //     if (!planetHolder.Value.TryGet(out GameObject planet))
        //     {
        //         return;
        //     }
        //
        //     Mesh mesh = planet.GetComponent<MeshFilter>().sharedMesh;
        //
        //     int[] meshTriangles = mesh.triangles;
        //     Vector3[] meshVertices = mesh.vertices;
        //
        //     for (int i = 0; i < meshTriangles.Length; i += 3)
        //     {
        //         DrawLine(meshVertices[meshTriangles[i + 0]], meshVertices[meshTriangles[i + 1]]);
        //         DrawLine(meshVertices[meshTriangles[i + 1]], meshVertices[meshTriangles[i + 2]]);
        //         DrawLine(meshVertices[meshTriangles[i + 2]], meshVertices[meshTriangles[i + 0]]);
        //     }
        // }
        //
        // private void DrawLine(Vector3 start, Vector3 end)
        // {
        //     if (Vector3.Distance(showCenter, start) > showMaxDistance)
        //     {
        //         return;
        //     }
        //     
        //     Vector3 center = (start + end) / 2;
        //
        //     float distance = Vector3.Distance(start, end);
        //
        //     distance = MathF.Round(distance, 2);
        //     
        //     UnityEditor.Handles.DrawLine(start, end, 1.0f);
        //     UnityEditor.Handles.Label(center, distance.ToString());
        // }
    }
}
