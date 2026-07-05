namespace ShelfLife.Catalog.Infrastructure.Persistence
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }

        public DateTime OccurredAt { get; set; }

        public string Type { get; set; } = default!;

        public string Payload { get; set; } = default!;

        public DateTime? ProcessedAt { get; set; }
    }
}
