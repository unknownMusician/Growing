using System;
using System.Threading;
using System.Threading.Tasks;
using AreYouFruits.Tasks.Unity;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsController : IDisposable
    {
        private readonly ResourceTransferersHolder resourceTransferersHolder;
        private readonly ResourceHolder resourceHolder;

        // TODO : Create abstract TokenSource and create StopTokenSource
        private readonly CancellationTokenSource cts = new();

        public ResourceBuildingsController(ResourceTransferersHolder resourceTransferersHolder,
            ResourceHolder resourceHolder)
        {
            this.resourceTransferersHolder = resourceTransferersHolder;
            this.resourceHolder = resourceHolder;

            RunCreationCycle();
        }

        private void RunCreationCycle()
        {
            ResourceCreationCycle().CatchAndLog();
        }

        private async Task ResourceCreationCycle()
        {
            while (!cts.IsCancellationRequested)
            {
                CreateResources();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private void CreateResources()
        {
            foreach (var resourceTransferer in resourceTransferersHolder.Values)
            {
                resourceHolder.Add(resourceTransferer.ResourceType, resourceTransferer.AmountOfResourcePerSecond);
            }
        }

        public void Dispose()
        {
            cts.Cancel();
        }
    }
}