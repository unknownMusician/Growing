using AreYouFruits.InitializerGeneration;
using UnityEngine;

namespace Player
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class CameraPlayerFollower : MonoBehaviour
    {
        [SerializeField] private float height;
        
        [GenerateInitializer] private PlayerHolder playerHolder;

        private void Update()
        {
            if (!playerHolder.Value.TryGet(out var player))
            {
                return;
            }

            var playerPosition = player.transform.position;
            var cameraDirection = (playerPosition - Vector3.zero).normalized;

            var cameraPosition = playerPosition + cameraDirection * height;
            transform.position = cameraPosition;
            transform.forward = -cameraDirection;
        }
    }
}