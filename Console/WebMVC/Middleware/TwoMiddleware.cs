using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMVC.Attributes;

namespace WebMVC.Middleware
{
    [MiddlewareRegister(Sort = 200)]
    public class TwoMiddleware : IMiddleware
    {
        private readonly ILogger<TwoMiddleware> logger;

        public TwoMiddleware(ILogger<TwoMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            logger.LogWarning("Two Middleware");
            await next(context);
        }
    }
}
