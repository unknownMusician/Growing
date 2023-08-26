using UnityEngine;

namespace Growing.PlanetGeneration
{
    public interface IIcosahedronDataProvider
    {
        public Vector3[] Vertices { get; }
        public int[] Triangles { get; }
    }
}
