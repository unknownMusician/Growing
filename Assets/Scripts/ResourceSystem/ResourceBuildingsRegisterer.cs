using Growing.Events;
using Growing.Messages;
using UnityEngine;

namespace Growing.ResourceSystem
{
    public sealed class ResourceBuildingsRegisterer
    {
        private readonly ResourceBuildingsHolder resourceBuildingsHolder;
        
        public ResourceBuildingsRegisterer(ResourceBuildingsHolder resourceBuildingsHolder, EventBus eventBus)
        {
            this.resourceBuildingsHolder = resourceBuildingsHolder;
            
            eventBus.Subscribe<BuildingPlacedEvent>(this, HandleBuildingPlaced);
        }

        // TODO : Add ResourceConsumer handling (after resource consumption is implemented)
        private void HandleBuildingPlaced(BuildingPlacedEvent buildingPlacedEvent)
        {
            Debug.Log("Building is created!");
            if (buildingPlacedEvent.Building.TryGetComponent<ResourceCreator>(out var resourceCreator))
            {
                Debug.Log("Building is ResourceCreator");
                resourceBuildingsHolder.ResourceCreators.Add(resourceCreator);
            }
        }
    }
}