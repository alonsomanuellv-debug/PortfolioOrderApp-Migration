
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PortfolioOrderApp.Net8.Infrastructure
{
    public class ComplianceTraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ComplianceTraceMiddleware> _logger;

        public ComplianceTraceMiddleware(
            RequestDelegate next,
            ILogger<ComplianceTraceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation(
                    "{Path} tomó {ElapsedMs} ms",
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
