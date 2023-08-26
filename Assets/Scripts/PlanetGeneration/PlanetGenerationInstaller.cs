using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.ContextInitialization;
using AreYouFruits.DependencyInjection.TypeResolvers;

namespace Growing.PlanetGeneration
{
    public static class PlanetGenerationInstaller
    {
        [ContextInitializer]
        public static void Install(IDiContainer container)
        {
            container.BindToInjectedConstructorLazySingleton<PlanetGenerator>();
            container.BindToInjectedConstructorLazySingleton<PlanetHolder>();
        }
    }
}
