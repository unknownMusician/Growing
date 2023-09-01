using UnityEngine;
using UnityEngine.UI;

namespace Growing.UserInterface
{
    public class BuilderMenu : MonoBehaviour
    {
        [SerializeField] private Button openBuilderMenuButton;

        private void Awake()
        {
            openBuilderMenuButton.onClick.AddListener(OpenBuilderMenu);
        }

        private void OpenBuilderMenu()
        {
            Debug.Log("Builder menu opened!");
        }
    }
}