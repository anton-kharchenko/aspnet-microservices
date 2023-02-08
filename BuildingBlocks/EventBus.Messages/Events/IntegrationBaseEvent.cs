namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public Guid Id { get; private set; }

    public DateTime CreationTime { get; private set; }

    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id, DateTime creationTime)
    {
        Id = id;
        CreationTime = creationTime;
    }
}
