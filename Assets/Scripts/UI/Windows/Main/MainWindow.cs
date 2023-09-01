using AreYouFruits.InitializerGeneration;
using Growing.UI.WindowSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Growing.UI.Windows.Main
{
    [GeneratedInitializerName("Inject")]
    public sealed partial class MainWindow : MonoBehaviour
    {
        [SerializeField] private Button builderWindowButton;

        [GenerateInitializer] private WindowOpener windowOpener;
        
        private void OnEnable()
        {
            builderWindowButton.onClick.AddListener(OpenBuilderWindow);
        }

        private void OnDisable()
        {
            builderWindowButton.onClick.RemoveListener(OpenBuilderWindow);
        }

        private void OpenBuilderWindow()
        {
            windowOpener.OpenWindow(WindowType.Build);
        }
    }
}
