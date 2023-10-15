using System;
using System.Collections.Generic;
using UnityEngine;

namespace Growing.ResourceSystem
{
    public sealed class ResourceHolder
    {
        // TODO : At the moment we are limited with float type of value. But maybe we will need integers, for amount of people for example. Think about component-based approach
        private readonly Dictionary<ResourceType, float> amountOfResourcesByType = new()
        {
            { ResourceType.Money , 0 }
        };

        public void Add(ResourceType resourceType, float amount)
        {
            ThrowIfNotContains(resourceType);
            amountOfResourcesByType[resourceType] += amount;
            
            var roundedAmount = Math.Round(amountOfResourcesByType[resourceType], 2);
            Debug.Log($"Current amount of {resourceType} is {roundedAmount}");
        }

        public bool IsEnough(ResourceType resourceType, float amount)
        {
            ThrowIfNotContains(resourceType);
            return amountOfResourcesByType[resourceType] > amount;
        }

        private void ThrowIfNotContains(ResourceType resourceType)
        {
            if (!amountOfResourcesByType.ContainsKey(resourceType))
            {
                throw new KeyNotFoundException(
                    $"Dictionary {nameof(amountOfResourcesByType)} doesn't have {resourceType.ToString()}");
            }
        }
    }
}