using AreYouFruits.InitializerGeneration;
using AreYouFruits.MonoBehaviourUtils.Unity;
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
            foreach (BuildingInfo buildingInfo in buildingsSettings.BuildingInfos)
            {
                GameObject buildingBuildViewObject = Instantiate(prefab, parent);

                var buildingBuildView = buildingBuildViewObject.GetComponentOrThrow<BuildingBuildView>();
                
                buildingBuildView.Initialize(buildingInfo.BuildViewIcon);
            }
        }
    }
}
