namespace MQ;

public interface IEventBusSubscriptionsManager
{
    bool HasSubscriptionsForEvent(string eventName);
    public void AddSubscription<T, TH>() where T : IntegrationBaseEvent where TH : IIntegrationEventHandler<T>;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
}