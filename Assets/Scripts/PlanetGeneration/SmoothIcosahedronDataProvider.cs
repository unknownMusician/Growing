using UnityEngine;

namespace Growing.PlanetGeneration
{
    public sealed class SmoothIcosahedronDataProvider : IIcosahedronDataProvider
    {
        private const float X = 0.525731112119133606f;
        private const float Z = 0.850650808352039932f;
        private const float N = 0.0f;
        
        public Vector3[] Vertices { get; } =
        {
            new(-X, N, Z), new(X, N, Z), new(-X, N, -Z), new(X, N, -Z),
            new(N, Z, X), new(N, Z, -X), new(N, -Z, X), new(N, -Z, -X),
            new(Z, X, N), new(-Z, X,  N), new(Z, -X, N), new(-Z, -X,  N),
        };
        
        public int[] Triangles { get; } =
        {
            0,  1,  4,   0,  4, 9,   9, 4,  5,    4, 8, 5,   4,  1, 8,
            8,  1, 10,   8, 10, 3,   5, 8,  3,    5, 3, 2,   2,  3, 7,
            7,  3, 10,   7, 10, 6,   7, 6, 11,   11, 6, 0,   0,  6, 1,
            6, 10,  1,   9, 11, 0,   9, 2, 11,    9, 5, 2,   7, 11, 2,
        };
    }
}
