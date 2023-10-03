using System.Collections.Generic;
using Growing.Builder;
using UnityEngine;

namespace Growing.ResourceSystem
{
    public class ResourceBuildingsHolder
    {
        private readonly Dictionary<ResourceType, List<ResourceCreator>> resourceCreatorsByResourceTypes = new();

        // TODO : Use ResourceBuildingsRegisterer
        public IReadOnlyDictionary<ResourceType, List<ResourceCreator>> ResourceCreatorsByResourceTypes =>
            resourceCreatorsByResourceTypes;

        public ResourceBuildingsHolder(BuildingPlacer buildingPlacer)
        {
            buildingPlacer.OnBuildingPlaced += HandleBuildingPlaced;
        }

        // TODO : Add ResourceConsumer handling (after resource consumption is implemented)
        private void HandleBuildingPlaced(GameObject building)
        {
            Debug.Log("Building is created!");
            if (building.TryGetComponent<ResourceCreator>(out var resourceCreator))
            {
                Debug.Log("Building is ResourceCreator");
                AddResourceCreatorToDictionary(resourceCreator);
            }
        }

        private void AddResourceCreatorToDictionary(ResourceCreator resourceCreator)
        {
            if (!resourceCreatorsByResourceTypes.ContainsKey(resourceCreator.ResourceType))
            {
                resourceCreatorsByResourceTypes[resourceCreator.ResourceType] = new List<ResourceCreator>();
            }

            resourceCreatorsByResourceTypes[resourceCreator.ResourceType].Add(resourceCreator);
        }
    }
}