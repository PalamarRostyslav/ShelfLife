using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace ShelfLife.Messaging
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly RabbitMqConnectionManager _connectionManager;
        private readonly string _exchangeName;

        public RabbitMqEventPublisher(RabbitMqConnectionManager connectionManager, IOptions<RabbitMqOptions> options)
        {
            _connectionManager = connectionManager;
            _exchangeName = options.Value.ExchangeName;
        }

        public async Task PublishAsync(string eventType, string payloadJson, CancellationToken cancellationToken = default)
        {
            var connection = await _connectionManager.GetConnectionAsync(cancellationToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true, cancellationToken: cancellationToken);

            var routingKey = eventType.ToLowerInvariant();
            var body = Encoding.UTF8.GetBytes(payloadJson);

            var props = new BasicProperties
            {
                Persistent = true,
                Type = eventType,
                MessageId = Guid.NewGuid().ToString(),
            };

            await channel.BasicPublishAsync(_exchangeName, routingKey, mandatory: false, props, body, cancellationToken);
        }
    }
}
