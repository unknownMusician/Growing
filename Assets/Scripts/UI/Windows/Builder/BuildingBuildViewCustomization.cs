using UnityEngine;
using UnityEngine.UI;

namespace Growing.UI.Windows.Builder
{
    // TODO : Name it better!
    public sealed class BuildingBuildViewCustomization : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
    }
}