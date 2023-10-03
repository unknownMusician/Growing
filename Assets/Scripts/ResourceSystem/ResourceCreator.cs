using UnityEngine;

namespace Growing.ResourceSystem
{
    public class ResourceCreator : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private float amountOfResourcesPerSecond;

        public ResourceType ResourceType => resourceType;
        public float AmountOfResourcePerSecond => amountOfResourcesPerSecond;
    }
}