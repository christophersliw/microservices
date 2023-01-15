namespace MQ;

public abstract class IntegrationBaseEvent
{
    public Guid EventId { get; set; }
    public DateTime CreatedEventDate { get; set; }
}