using Growing.Events;
using Growing.Messages;
using UnityEngine;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsRegisterer
    {
        private readonly ResourceTransferersHolder resourceTransferersHolder;
        
        public ResourceBuildingsRegisterer(ResourceTransferersHolder resourceTransferersHolder, EventBus eventBus)
        {
            this.resourceTransferersHolder = resourceTransferersHolder;
            
            eventBus.Subscribe<BuildingPlacedEvent>(this, HandleBuildingPlaced);
        }

        // TODO : Add ResourceConsumer handling (after resource consumption is implemented)
        private void HandleBuildingPlaced(BuildingPlacedEvent buildingPlacedEvent)
        {
            Debug.Log("Building is created!");
            if (buildingPlacedEvent.Building.TryGetComponent<ResourceTransferer>(out var resourceTransferer))
            {
                Debug.Log("Building is ResourceTransferer");
                resourceTransferersHolder.Values.Add(resourceTransferer);
            }
        }
    }
}