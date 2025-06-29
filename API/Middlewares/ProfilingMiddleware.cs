using System.Diagnostics;

namespace API.Middlewares
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;

        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();

            const string logMessageTemplate = "Request {Path} took: [{ElapsedMilliseconds}]ms.";
            _logger.LogInformation(logMessageTemplate, context.Request.Path, stopwatch.ElapsedMilliseconds);
        }
    }
    
}
