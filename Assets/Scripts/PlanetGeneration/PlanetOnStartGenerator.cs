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
        
        private void Start()
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
        //     for (int i = 0; i < mesh.vertices.Length; i += 3)
        //     {
        //         DrawLine(mesh.vertices[i + 0], mesh.vertices[i + 1]);
        //         DrawLine(mesh.vertices[i + 1], mesh.vertices[i + 2]);
        //         DrawLine(mesh.vertices[i + 2], mesh.vertices[i + 0]);
        //     }
        // }
        //
        // private void DrawLine(Vector3 start, Vector3 end)
        // {
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
