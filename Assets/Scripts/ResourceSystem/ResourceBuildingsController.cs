using System;
using System.Threading;
using System.Threading.Tasks;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsController : IDisposable
    {
        private readonly ResourceBuildingsHolder resourceBuildingsHolder;
        private readonly ResourceHolder resourceHolder;

        private readonly CancellationTokenSource cts = new();

        public ResourceBuildingsController(ResourceBuildingsHolder resourceBuildingsHolder,
            ResourceHolder resourceHolder)
        {
            this.resourceBuildingsHolder = resourceBuildingsHolder;
            this.resourceHolder = resourceHolder;

            RunCreationCycle();
        }

        private void RunCreationCycle()
        {
            Task.Run(ResourceCreationCycle);
        }

        private async Task ResourceCreationCycle()
        {
            while (!cts.IsCancellationRequested)
            {
                CreateResources();
                await Task.Delay(1000);
            }
        }

        private void CreateResources()
        {
            foreach (var resourceTransferer in resourceBuildingsHolder.ResourceTransferers)
            {
                resourceHolder.Add(resourceTransferer.ResourceType, resourceTransferer.AmountOfResourcePerSecond);
            }
        }

        public void Dispose()
        {
            cts.Dispose();
            cts.Cancel();
        }
    }
}