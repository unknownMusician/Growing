using AreYouFruits.ConstructorGeneration;
using UnityEngine;

namespace Growing.Utils
{
    public partial struct Triangle
    {
        [GenerateConstructor] public Vector3 Vertex0;
        [GenerateConstructor] public Vector3 Vertex1;
        [GenerateConstructor] public Vector3 Vertex2;
    }
}