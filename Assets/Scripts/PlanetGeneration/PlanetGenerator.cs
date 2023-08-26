using AreYouFruits.ConstructorGeneration;
using UnityEngine;

namespace Growing.PlanetGeneration
{
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
            var mesh = new Mesh
            {
                name = "Generated Planet",
                vertices = icosahedronDataProvider.Vertices,
                triangles = icosahedronDataProvider.Triangles,
            };
            
            mesh.RecalculateNormals();

            return mesh;
        }
    }
}
