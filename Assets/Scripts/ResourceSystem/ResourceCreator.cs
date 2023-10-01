using System.Threading.Tasks;
using AreYouFruits.InitializerGeneration;
using UnityEngine;

namespace Growing.ResourceSystem
{
    [GeneratedInitializerName("Inject")]
    public partial class ResourceCreator : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private float amountOfResourcesPerSecond;

        [GenerateInitializer] private ResourceHolder resourceHolder;

        private bool isCreating = true;
        
        private async void Start()
        {
            await Task.Run(Cycle);
        }

        private void OnDestroy()
        {
            isCreating = false;
        }

        private async Task Cycle()
        {
            while (isCreating)
            {
                resourceHolder.Add(resourceType, amountOfResourcesPerSecond);
                await Task.Delay(1000);
            }
        }
    }
}