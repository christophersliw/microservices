namespace MQ;

public interface IIntegrationEventBus
{
    void Publish(IntegrationBaseEvent @event);
    void Subscribe<T, TH>() where T : IntegrationBaseEvent where TH : IIntegrationEventHandler<T>;
}