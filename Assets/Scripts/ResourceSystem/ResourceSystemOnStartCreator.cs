using AreYouFruits.InitializerGeneration;
using UnityEngine;

namespace Growing.ResourceSystem
{
    [GeneratedInitializerName("Inject")]
    public partial class ResourceSystemOnStartCreator : MonoBehaviour
    {
        [GenerateInitializer] private ResourceBuildingsController resourceBuildingsController;
        [GenerateInitializer] private ResourceBuildingsRegisterer resourceBuildingsRegisterer;
    }
}