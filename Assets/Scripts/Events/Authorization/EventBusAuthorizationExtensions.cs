namespace Growing.Events.Authorization
{
    public static class EventBusAuthorizationExtensions
    {
        public static EventBusAuthorisedAccess GetAuthorizedAccess(this EventBus eventBus, object subscriber)
        {
            return new EventBusAuthorisedAccess(eventBus, subscriber);
        }
    }
}
