using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Growing.ResourceSystem
{
    public class ResourceBuildingsController : IDisposable
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
            foreach (var (resourceType, resourceCreators) in resourceBuildingsHolder.ResourceCreatorsByResourceTypes)
            {
                if (resourceCreators.Count == 0)
                {
                    continue;
                }

                float amountOfCreatedResource = resourceCreators
                    .Sum(resourceCreator => resourceCreator.AmountOfResourcePerSecond);

                resourceHolder.Add(resourceType, amountOfCreatedResource);
            }
        }

        public void Dispose()
        {
            cts.Dispose();
            cts.Cancel();
        }
    }
}