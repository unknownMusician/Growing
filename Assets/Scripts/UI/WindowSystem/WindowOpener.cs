using AreYouFruits.Nullability;

namespace Growing.UI.WindowSystem
{
    public sealed class WindowOpener
    {
        private readonly WindowRegistryHolder windowRegistryHolder;

        private Optional<WindowPair> openedWindow;

        public WindowOpener(WindowRegistryHolder windowRegistryHolder)
        {
            this.windowRegistryHolder = windowRegistryHolder;
        }

        public void OpenWindow(WindowType windowType)
        {
            CloseWindow();

            WindowComponent window = windowRegistryHolder.Value.Windows[windowType];

            openedWindow = new WindowPair
            {
                Type = windowType,
                Window = window,
            };
            
            window.Open();
        }

        public bool CloseWindow()
        {
            if (!openedWindow.TryGet(out var windowPair))
            {
                return false;
            }

            windowPair.Window.Close();
            openedWindow = default;

            return true;
        }
    }
}