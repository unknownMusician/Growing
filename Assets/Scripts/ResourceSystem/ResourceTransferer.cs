using UnityEngine;

namespace Growing.ResourceSystem
{
    public sealed class ResourceTransferer : MonoBehaviour
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        [field: SerializeField] public float AmountOfResourcePerSecond { get; private set; }
    }
}