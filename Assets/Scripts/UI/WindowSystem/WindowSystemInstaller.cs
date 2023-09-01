using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.ContextInitialization;
using AreYouFruits.DependencyInjection.TypeResolvers;

namespace Growing.UI.WindowSystem
{
    public static class WindowSystemInstaller
    {
        [ContextInitializer]
        public static void Install(IDiContainer container)
        {
            container.BindToInjectedConstructorLazySingleton<WindowRegistryHolder>();
            container.BindToInjectedConstructorLazySingleton<WindowOpener>();
        }
    }
}