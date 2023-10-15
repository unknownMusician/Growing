using AreYouFruits.Assertions;
using AreYouFruits.InitializerGeneration;
using Growing.Settings;
using UnityEngine;

namespace Player
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class PlayerOnStartSpawner : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;

        [GenerateInitializer] private PlanetGenerationSettings planetGenerationSettings;
        [GenerateInitializer] private PlayerHolder playerHolder;

        private void Start()
        {
            playerHolder.Value.Expect(default);
            playerHolder.Value = Instantiate(playerPrefab, Vector3.up * planetGenerationSettings.Radius,
                Quaternion.identity);
        }
    }
}