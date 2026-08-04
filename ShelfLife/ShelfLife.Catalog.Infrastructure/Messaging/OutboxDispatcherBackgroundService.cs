using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShelfLife.Catalog.Infrastructure.Persistence;
using ShelfLife.Messaging;

namespace ShelfLife.Catalog.Infrastructure.Messaging
{
    public class OutboxDispatcherBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxDispatcherBackgroundService> _logger;
        private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);

        public OutboxDispatcherBackgroundService(IServiceScopeFactory scopeFactory, ILogger<OutboxDispatcherBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DispatchPendingAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while dispatching outbox events.");
                }

                await Task.Delay(PollInterval, stoppingToken);
            }
        }

        private async Task DispatchPendingAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

            var pending = await db.OutboxMessages
                .Where(m => m.ProcessedAt == null)
                .OrderBy(m => m.OccurredAt)
                .Take(20)
                .ToListAsync(cancellationToken);

            if (pending.Count == 0)
            {
                return;
            }

            foreach (var message in pending)
            {
                try
                {
                    await publisher.PublishAsync(message.Type, message.Payload, cancellationToken);
                    message.ProcessedAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to publish outbox message with ID {MessageId}.", message.Id);
                }
            }

            await db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Dispatched {Count} outbox messages.", pending.Count);
        }
    }
}
