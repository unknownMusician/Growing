using AreYouFruits.InitializerGeneration;
using Growing.Builder;
using Growing.UI.WindowSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Growing.UI.Windows.Builder
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class BuildingOnClickChooser : MonoBehaviour
    {
        [SerializeField] private Button button;

        [GenerateInitializer] private WindowOpener windowOpener;
        [GenerateInitializer] private PlacedBuildingInfoHolder placedBuildingInfoHolder;

        private BuildingInfo buildingInfo;
        
        private void Awake()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            windowOpener.OpenWindow(WindowType.Main);
            placedBuildingInfoHolder.CurrentBuildingInfo = buildingInfo;
        }
        
        public void Initialize(BuildingInfo buildingInfo)
        {
            this.buildingInfo = buildingInfo;
        }
    }
}