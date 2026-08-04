namespace ShelfLife.Messaging
{
    public interface IEventPublisher
    {
        Task PublishAsync(string eventType, string payloadJson, CancellationToken cancellationToken = default);
    }
}
