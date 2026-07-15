namespace ShelfLife.SharedKernel.Events
{
    public record BookFinished(Guid bookId, DateTime finishedAt) : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}
