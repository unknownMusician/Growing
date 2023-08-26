using AreYouFruits.ConstructorGeneration;
using UnityEngine;

namespace Growing.PlanetGeneration
{
    public sealed partial class PlanetGenerator
    {
        [GenerateConstructor] private readonly PlanetGenerationSettings planetGenerationSettings;
        [GenerateConstructor] private readonly PlanetHolder planetHolder;
        
        public void Generate()
        {
            _ = planetGenerationSettings.Detailing;

            var gameObject = new GameObject();

            planetHolder.Value = gameObject;
        }
    }
}
