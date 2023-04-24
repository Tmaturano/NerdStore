using NS.Core.Messages;

namespace NS.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; }

    protected Entity() => Id = Guid.NewGuid();

    private List<Event> _events;
    public IReadOnlyCollection<Event> Notifications => _events?.AsReadOnly();

    /// <summary>
    /// Add and event in the memory so it can be sent later.
    /// </summary>
    /// <param name="newEvent"></param>
    public void AddEvent(Event newEvent)
    {
        _events ??= new List<Event>();
        _events.Add(newEvent);
    }

    public void RemoveEvent(Event eventItem) => _events?.Remove(eventItem);

    public void ClearEvents() => _events?.Clear();

    #region Overrides
    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    /// <summary>
    /// each instance has a random hashcode
    /// there's a little chance of this code be equals between instance, so we modify it
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
    #endregion
}
