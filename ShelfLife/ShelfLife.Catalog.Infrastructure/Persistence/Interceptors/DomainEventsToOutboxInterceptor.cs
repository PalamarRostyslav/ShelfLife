using Microsoft.EntityFrameworkCore.Diagnostics;
using ShelfLife.SharedKernel.Events;
using System.Text.Json;

namespace ShelfLife.Catalog.Infrastructure.Persistence.Interceptors
{
    public class DomainEventsToOutboxInterceptor : SaveChangesInterceptor
    {
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context is null) return result;

            var entitiesWithEvents = context.ChangeTracker
                .Entries<IHasDomainEvents>()
                .Where(e => e.Entity.DomainEvents.Count > 0)
                .Select(e => e.Entity)
                .ToList();

            var outboxMessages = entitiesWithEvents
                .SelectMany(e => e.DomainEvents)
                .Select(domainEvent => new OutboxMessage
                {
                    Id = domainEvent.EventId,
                    OccurredAt = domainEvent.OccurredAt,
                    Type = domainEvent.GetType().Name!,
                    Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), _jsonOptions),
                    ProcessedAt = null
                })
                .ToList();

            context.Set<OutboxMessage>().AddRange(outboxMessages);

            entitiesWithEvents.ForEach(e => e.ClearDomainEvents());

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
