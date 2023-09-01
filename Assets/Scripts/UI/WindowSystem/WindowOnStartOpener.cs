using AreYouFruits.DependencyInjection;
using UnityEngine;

namespace Growing.UI.WindowSystem
{
    public sealed class WindowOnStartOpener : MonoBehaviour
    {
        [SerializeField] private WindowType windowType;

        private WindowOpener windowOpener;

        public void Initialize(WindowOpener windowOpener)
        {
            this.windowOpener = windowOpener;
        }
        
        private void Awake()
        {
            Context.Container.Resolve(out windowOpener);
        }

        private void Start()
        {
            windowOpener.OpenWindow(windowType);
        }
    }
}