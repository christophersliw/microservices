namespace MQ;

public class EventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
    private readonly List<Type> _eventTypes;

    public EventBusSubscriptionsManager()
    {
        _handlers = new Dictionary<string, List<SubscriptionInfo>>();
        _eventTypes = new List<Type>();
    }
    public bool HasSubscriptionsForEvent(string eventName)
    {
        return _handlers.ContainsKey(eventName);
    }
    
    public void AddSubscription<T, TH>() where T : IntegrationBaseEvent where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        Type handlerType = typeof(TH);
        
        if (!HasSubscriptionsForEvent(eventName))
        {
            _handlers.Add(eventName, new List<SubscriptionInfo>());
        }
        if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
        }
        
        _handlers[eventName].Add(new SubscriptionInfo(handlerType));

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];
    
    public Type GetEventTypeByName(string eventName)
    {
        return _eventTypes.SingleOrDefault(e => e.Name == eventName);
    }
}