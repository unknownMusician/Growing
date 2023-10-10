using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.ContextInitialization;
using AreYouFruits.DependencyInjection.TypeResolvers;

namespace Growing.Events.DI
{
    public static class EventsInstaller
    {
        [ContextInitializer]
        public static void Install(IDiContainer container)
        {
            container.BindToInjectedConstructorLazySingleton<EventBus>();
        }
    }
}
