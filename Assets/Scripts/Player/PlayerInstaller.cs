using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.ContextInitialization;
using AreYouFruits.DependencyInjection.TypeResolvers;

namespace Player
{
    public static class PlayerInstaller
    {
        [ContextInitializer]
        public static void Install(IDiContainer container)
        {
            container.BindToInjectedConstructorLazySingleton<PlayerHolder>();
        }
    }
}