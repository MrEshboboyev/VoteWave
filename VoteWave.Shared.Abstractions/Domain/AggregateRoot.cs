namespace VoteWave.Shared.Abstractions.Domain;

public abstract class AggregateRoot<T>
{
    public T Id { get; protected set; }
    public int Version { get; protected set; }
    public IEnumerable<IDomainEvent> Events => _events;

    private readonly List<IDomainEvent> _events = [];
    private bool _versionIncremented;

    protected void AddEvent(IDomainEvent @event)
    {
        if (_events.Count != 0 && !_versionIncremented)
        {
            Version++;
            _versionIncremented = true;

            AddEvent(@event);
        }
    }

    protected void IncrementVersion()
    {
        if (_versionIncremented)
            return;

        Version++;
        _versionIncremented = false;
    }
}
