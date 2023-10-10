using AreYouFruits.InitializerGeneration;
using AreYouFruits.MonoBehaviourUtils.Unity;
using Growing.Builder;
using UnityEngine;

namespace Growing.UI.Windows.Builder
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class BuildingsOnStartFiller : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject prefab;

        [GenerateInitializer] private BuildingsSettings buildingsSettings;

        private void Start()
        {
            foreach (var buildingInfo in buildingsSettings.BuildingInfos)
            {
                var buildingBuildViewObject = Instantiate(prefab, parent);

                var buildingBuildView = buildingBuildViewObject.GetComponentOrThrow<BuildingBuildView>();
                var buildingOnClickChooser = buildingBuildViewObject.GetComponentOrThrow<BuildingOnClickChooser>();
                
                buildingBuildView.Initialize(buildingInfo.BuildViewIcon);
                buildingOnClickChooser.Initialize(buildingInfo);
            }
        }
    }
}
