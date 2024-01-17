using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ClayDoorsController.Middlewares
{
    public class LoggerExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<LoggerExceptionHandler> logger;

        public LoggerExceptionHandler(ILogger<LoggerExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogDebug(exception, null);
            return false;
        }
    }
}
