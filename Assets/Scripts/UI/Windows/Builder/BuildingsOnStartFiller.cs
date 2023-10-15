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
            InstantiateBuildings();
        }

        private void InstantiateBuildings()
        {
            foreach (var buildingInfo in buildingsSettings.BuildingInfos)
            {
                InstantiateBuilding(buildingInfo);
            }
        }

        private void InstantiateBuilding(BuildingInfo buildingInfo)
        {
            var buildingBuildViewObject = Instantiate(prefab, parent);

            var buildingBuildViewCustomization = InstantiateBuildingBuildViewCustomization(buildingInfo,
                buildingBuildViewObject);
            InstantiateBuildingOnClickChooser(buildingBuildViewObject, buildingInfo, buildingBuildViewCustomization);
        }

        private BuildingBuildViewCustomization InstantiateBuildingBuildViewCustomization(BuildingInfo buildingInfo, 
            GameObject buildingBuildViewObject)
        {
            var buildingIcon = Instantiate(buildingInfo.BuildViewIconPrefab, buildingBuildViewObject.transform);
            buildingIcon.transform.localPosition = Vector3.one;
            return buildingIcon.GetComponentOrThrow<BuildingBuildViewCustomization>();
        }

        private void InstantiateBuildingOnClickChooser(GameObject buildingBuildViewObject, BuildingInfo buildingInfo, 
            BuildingBuildViewCustomization buildingBuildViewCustomization)
        {
            var buildingOnClickChooser = buildingBuildViewObject.GetComponentOrThrow<BuildingOnClickChooser>();
            buildingOnClickChooser.Initialize(buildingInfo);
            buildingOnClickChooser.SetButton(buildingBuildViewCustomization.Button);
        }
    }
}
