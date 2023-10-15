using System;
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
        private Button buttonn;
        
        [GenerateInitializer] private WindowOpener windowOpener;
        [GenerateInitializer] private PlacingBuildingInfoHolder placingBuildingInfoHolder;

        private BuildingInfo buildingInfo;

        public void SetButton(Button button)
        {
            TryUnsubscribeButton();
            
            this.buttonn = button;
            
            button.onClick.AddListener(HandleClick);
        }

        private void TryUnsubscribeButton()
        {
            if (buttonn != null)
            {
                buttonn.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            windowOpener.OpenWindow(WindowType.Main);
            placingBuildingInfoHolder.Value = buildingInfo;
        }
        
        public void Initialize(BuildingInfo buildingInfo)
        {
            this.buildingInfo = buildingInfo;
        }

        private void OnDestroy()
        {
            TryUnsubscribeButton();
        }
    }
}