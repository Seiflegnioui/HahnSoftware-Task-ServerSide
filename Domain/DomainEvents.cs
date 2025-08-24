using MediatR;

namespace hahn.Domain
{
    public static class DomainEvents
{
    public static List<INotification> Events = new();

    public static void Raise(INotification eventItem)
    {
        Events.Add(eventItem);
    }

    public static void Clear() => Events.Clear();
}

}