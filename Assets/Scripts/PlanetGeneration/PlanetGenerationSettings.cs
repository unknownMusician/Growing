using UnityEngine;

namespace Growing.PlanetGeneration
{
    [CreateAssetMenu(menuName = "Growing/" + nameof(PlanetGenerationSettings), fileName = nameof(PlanetGenerationSettings), order = 0)]
    public sealed class PlanetGenerationSettings : ScriptableObject
    {
        [field: SerializeField] public int Detailing { get; private set; }
    }
}
