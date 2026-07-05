namespace ShelfLife.SharedKernel.Events
{
    public record SessionLogged(Guid SessionId, Guid BookId, DateTime Date, int PagesRead) : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}
