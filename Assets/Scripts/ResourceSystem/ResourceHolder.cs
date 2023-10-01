using System;
using System.Collections.Generic;
using UnityEngine;

namespace Growing.ResourceSystem
{
    public class ResourceHolder
    {
        private readonly Dictionary<ResourceType, float> amountOfResourcesByType = new()
        {
            { ResourceType.Money , 0 }
        };

        public void Add(ResourceType resourceType, float amount)
        {
            CheckForResourceOrThrow(resourceType);
            amountOfResourcesByType[resourceType] += amount;
            
            double roundedAmount = Math.Round(amountOfResourcesByType[resourceType], 2);
            Debug.Log($"Current amount of {resourceType} is {roundedAmount}");
        }

        public bool IsEnough(ResourceType resourceType, float amount)
        {
            CheckForResourceOrThrow(resourceType);
            return amountOfResourcesByType[resourceType] > amount;
        }

        public void Withdraw(ResourceType resourceType, float amount)
        {
            CheckForResourceOrThrow(resourceType);
            
            if (amountOfResourcesByType[resourceType] < amount)
            {
                Debug.LogError($"You've tried to withdraw {amount} {resourceType.ToString()}, " +
                               $"but you have only {amountOfResourcesByType[resourceType]}");
                return;
            }
            
            amountOfResourcesByType[resourceType] -= amount;
        }

        private void CheckForResourceOrThrow(ResourceType resourceType)
        {
            if (!amountOfResourcesByType.ContainsKey(resourceType))
            {
                throw new KeyNotFoundException(
                    $"Dictionary {nameof(amountOfResourcesByType)} doesn't have {resourceType.ToString()}");
            }
        }
    }
}