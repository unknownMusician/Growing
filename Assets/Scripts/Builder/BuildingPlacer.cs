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
        private const int AmountOfVerticesInTriangle = 3;
        private const int RaycastMaxDistance = int.MaxValue;

        [GenerateInitializer] private PlacingBuildingInfoHolder placingBuildingInfoHolder;
        [GenerateInitializer] private EventBus eventBus;
        
        private readonly Vector3[] verticesCoordinates = new Vector3[AmountOfVerticesInTriangle];
        
        private void Update()
        {
            if (IsPlacing() && Input.GetMouseButtonDown(0))
            {
                TryPlace();
            }
        }

        private bool IsPlacing()
        {
            return placingBuildingInfoHolder.Value.IsInitialized;
        }

        private void TryPlace()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit, RaycastMaxDistance))
            {
                var placementData = GetPlacementDataFromTriangleIndex(hit);
                PlaceBuilding(placementData);
            }
        }

        private PlacementData GetPlacementDataFromTriangleIndex(RaycastHit hit)
        {
            UpdateVerticesCoordinates(hit);

            var center = GetCenter(verticesCoordinates);
            var forward = Vector3.Cross(hit.normal, verticesCoordinates[0] - center);
            
            return new PlacementData(center, Quaternion.LookRotation(forward, hit.normal));
        }

        private void UpdateVerticesCoordinates(RaycastHit hit)
        {
            var mesh = hit.collider.gameObject.GetComponentOrThrow<MeshCollider>().sharedMesh;
            for (var i = 0; i < verticesCoordinates.Length; i++)
            {
                var vertexIndex = mesh.triangles[hit.triangleIndex * 3 + i];
                verticesCoordinates[i] = mesh.vertices[vertexIndex];
            }
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
            var building = Instantiate(placingBuildingInfoHolder.Value.GetOrThrow().Prefab,
                placementData.Position, placementData.Rotation);
            eventBus.Invoke(new BuildingPlacedEvent(building));
        }
    }
}