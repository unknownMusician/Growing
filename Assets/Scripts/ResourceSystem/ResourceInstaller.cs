using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.ContextInitialization;
using AreYouFruits.DependencyInjection.TypeResolvers;

namespace Growing.ResourceSystem
{
    public static class ResourcesInstaller
    {
        [ContextInitializer]
        public static void Install(IDiContainer container)
        {
            container.BindToInjectedConstructorLazySingleton<ResourceHolder>();
            container.BindToInjectedConstructorLazySingleton<ResourceTransferersHolder>();
            container.BindToInjectedConstructorLazySingleton<ResourceBuildingsController>();
            container.BindToInjectedConstructorLazySingleton<ResourceBuildingsRegisterer>();
        }
    }
}