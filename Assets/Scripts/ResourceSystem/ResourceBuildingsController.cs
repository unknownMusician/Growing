using System;
using System.Threading.Tasks;
using AreYouFruits.Tasks.Unity;
using Growing.Utils.Tokens;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsController : IDisposable
    {
        private readonly ResourceTransferersHolder resourceTransferersHolder;
        private readonly ResourceHolder resourceHolder;

        private readonly StopTokenSource stopTokenSource = new();

        public ResourceBuildingsController(ResourceTransferersHolder resourceTransferersHolder,
            ResourceHolder resourceHolder)
        {
            this.resourceTransferersHolder = resourceTransferersHolder;
            this.resourceHolder = resourceHolder;

            RunCreationCycle();
        }

        private void RunCreationCycle()
        {
            ResourceCreationCycle(stopTokenSource.Token).CatchAndLog();
        }

        private async Task ResourceCreationCycle(StopToken stopToken)
        {
            while (!stopToken.IsStopped)
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
            stopTokenSource.Stop();
        }
    }
}