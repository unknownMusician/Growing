using AreYouFruits.Assertions;
using AreYouFruits.InitializerGeneration;
using UnityEngine;

namespace Player
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class PlayerOnStartSpawner : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Vector3 spawnPosition;

        [GenerateInitializer] private PlayerHolder playerHolder;

        private void Start()
        {
            playerHolder.Value.Expect(default);
            playerHolder.Value = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}