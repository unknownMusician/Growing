using System;
using AreYouFruits.InitializerGeneration;
using AreYouFruits.MonoBehaviourUtils.Unity;
using Growing.Events;
using Growing.Messages;
using UnityEngine;

namespace Growing.Builder
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class BuildingPlacer : MonoBehaviour
    {
        private const int RaycastMaxDistance = int.MaxValue;

        [GenerateInitializer] private PlacedBuildingInfoHolder placedBuildingInfoHolder;
        [GenerateInitializer] private EventBus eventBus;
        
        private void Update()
        {
            if (IsPlacing())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TryToPlace();
                }
            }
        }

        private bool IsPlacing()
        {
            return placedBuildingInfoHolder.CurrentBuildingInfo.IsInitialized;
        }

        private void TryToPlace()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, RaycastMaxDistance))
            {
                PlacementData placementData = GetPlacementDataFromTriangleIndex(hit);
                PlaceBuilding(placementData);
            }
        }

        private PlacementData GetPlacementDataFromTriangleIndex(RaycastHit hit)
        {
            const int amountOfVerticesInTriangle = 3;

            Mesh mesh = hit.collider.gameObject.GetComponentOrThrow<MeshCollider>().sharedMesh;
            
            Span<Vector3> verticesCoordinates = stackalloc Vector3[amountOfVerticesInTriangle];
            for (int i = 0; i < verticesCoordinates.Length; i++)
            {
                int vertexIndex = mesh.triangles[hit.triangleIndex * 3 + i];
                verticesCoordinates[i] = mesh.vertices[vertexIndex];
            }

            Vector3 center = GetCenter(verticesCoordinates);
            // TODO : Maybe extract method???
            Vector3 forward = Vector3.Cross(hit.normal, verticesCoordinates[0] - center);
            
            return new PlacementData(center, Quaternion.LookRotation(forward, hit.normal));
        }

        private Vector3 GetCenter(ReadOnlySpan<Vector3> coordinates)
        {
            var sum = Vector3.zero;

            foreach (var coordinate in coordinates)
            {
                sum += coordinate;
            }
            
            return sum / coordinates.Length;
        }

        private void PlaceBuilding(PlacementData placementData)
        {
            var building = Instantiate(placedBuildingInfoHolder.CurrentBuildingInfo.GetOrThrow().Prefab,
                placementData.Position, placementData.Rotation);
            eventBus.Invoke(new BuildingPlacedEvent(building));
        }
    }
}