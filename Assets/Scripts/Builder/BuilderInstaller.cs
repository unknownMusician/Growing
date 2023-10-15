using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.ContextInitialization;
using AreYouFruits.DependencyInjection.TypeResolvers;

namespace Growing.Builder
{
    public static class BuilderInstaller
    {
        [ContextInitializer]
        public static void Install(IDiContainer container)
        {
            container.BindToInjectedConstructorLazySingleton<PlacingBuildingInfoHolder>();
            container.BindToInjectedConstructorLazySingleton<BuildingPlacer>();
        }
    }
}