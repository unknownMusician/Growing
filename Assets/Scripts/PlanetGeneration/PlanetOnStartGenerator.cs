using AreYouFruits.InitializerGeneration;
using UnityEngine;

namespace Growing.PlanetGeneration
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class PlanetOnStartGenerator : MonoBehaviour
    {
        [GenerateInitializer] private PlanetGenerator planetGenerator;

        private void Start()
        {
            planetGenerator.Generate();
        }
    }
}
