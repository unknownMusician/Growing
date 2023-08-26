using System.Linq;
using AreYouFruits.ConstructorGeneration;
using UnityEngine;

namespace Growing.PlanetGeneration
{
    public sealed partial class LowPolyIcosahedronDataProvider : IIcosahedronDataProvider
    {
        [GenerateConstructor] private readonly SmoothIcosahedronDataProvider smoothIcosahedronDataProvider;
        
        public Vector3[] Vertices => smoothIcosahedronDataProvider.Triangles.Select(t => smoothIcosahedronDataProvider.Vertices[t]).ToArray();

        public int[] Triangles => Vertices.Select((_, i) => i).ToArray();
    }
}
