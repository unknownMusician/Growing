using System.Collections.Generic;
using UnityEngine;

namespace Growing.UI.Windows.Builder
{
    [CreateAssetMenu(menuName = nameof(Growing) + "/" + nameof(BuildingsSettings), fileName = nameof(BuildingsSettings), order = 0)]
    public sealed class BuildingsSettings : ScriptableObject
    {
        [SerializeField] private BuildingInfo[] buildingInfos;

        public IReadOnlyCollection<BuildingInfo> BuildingInfos => buildingInfos;
    }
}
