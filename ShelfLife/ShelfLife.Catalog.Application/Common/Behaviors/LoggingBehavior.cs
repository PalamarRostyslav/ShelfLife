using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ShelfLife.Catalog.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var sw = Stopwatch.StartNew();

            _logger.LogInformation("Handling {RequestName} {@Request}", requestName, request);

            try
            {
                var response = await next();
                _logger.LogInformation("Handled {RequestName} in {ElapsedMs}ms", requestName, sw.ElapsedMilliseconds);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling {RequestName}", requestName);
                throw;
            }
        }
    }
}
