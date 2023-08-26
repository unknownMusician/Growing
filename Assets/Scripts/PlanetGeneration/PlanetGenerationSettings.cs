using UnityEngine;

namespace Growing.PlanetGeneration
{
    [CreateAssetMenu(menuName = "Growing/" + nameof(PlanetGenerationSettings), fileName = nameof(PlanetGenerationSettings), order = 0)]
    public sealed class PlanetGenerationSettings : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField, Min(1)] public int Detailing { get; private set; } = 1;
    }
}
