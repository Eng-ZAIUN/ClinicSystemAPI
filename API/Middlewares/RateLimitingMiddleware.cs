namespace API.Middlewares
{
    public class RateLimitingMiddleware(RequestDelegate next)
    {
        private static int _requestCount = 0;
        private static DateTime _lastRequstdate = DateTime.Now;
        public  async Task Invoke(HttpContext context)
        {
            _requestCount++;
            if(DateTime.Now.Subtract(_lastRequstdate).Seconds > 10)
            {
                _requestCount = 1;
                _lastRequstdate = DateTime.Now;
                await next(context);
            }
            else
            {
                if (_requestCount > 3)
                {
                    _lastRequstdate = DateTime.Now;
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Too many requests. Please try again later.");
                    return;
                }
                else
                {
                    _lastRequstdate = DateTime.Now;
                    await next(context);
                }
            }
           
        }
    }
    
}
