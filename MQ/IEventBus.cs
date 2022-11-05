namespace MQ;

public interface IEventBus
{
    void Publish(BaseEvent @event);
}