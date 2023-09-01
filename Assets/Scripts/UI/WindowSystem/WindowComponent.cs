using UnityEngine;

namespace Growing.UI.WindowSystem
{
    public sealed class WindowComponent : MonoBehaviour, IWindow
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}