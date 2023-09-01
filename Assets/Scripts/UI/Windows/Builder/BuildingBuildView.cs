using UnityEngine;
using UnityEngine.UI;

namespace Growing.UI.Windows.Builder
{
    public sealed class BuildingBuildView : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void Initialize(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}
