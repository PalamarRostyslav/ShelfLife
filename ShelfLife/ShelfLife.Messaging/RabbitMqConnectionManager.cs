using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ShelfLife.Messaging
{
    public class RabbitMqConnectionManager
    {
        private readonly RabbitMqOptions _options;
        private IConnection? _connection;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public RabbitMqConnectionManager(IOptions<RabbitMqOptions> options) => _options = options.Value;

        public async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
        {
            if (_connection is not null && _connection.IsOpen)
            {
                return _connection;
            }

            await _lock.WaitAsync(cancellationToken);
            try
            {
                if (_connection is not null && _connection.IsOpen)
                {
                    return _connection;
                }

                var factory = new ConnectionFactory
                {
                    HostName = _options.Host,
                    UserName = _options.Username,
                    Password = _options.Password
                };

                _connection = await factory.CreateConnectionAsync(cancellationToken);
                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection is not null)
            {
                await _connection.CloseAsync();
            }
        }
    }
}
