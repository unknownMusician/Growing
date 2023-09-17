using AreYouFruits.ConstructorGeneration;
using UnityEngine;

namespace Growing.Builder
{
    public partial struct PlacementData
    {
        [GenerateConstructor] public Vector3 Position;
        [GenerateConstructor] public Quaternion Rotation;
    }
}