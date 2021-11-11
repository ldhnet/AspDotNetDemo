using Framework.Utility.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks; 

namespace WebMVC.Middleware
{
    [MiddlewareRegister(Sort = 300)]
    public class ThreeMiddleware : IMiddleware
    {
        private readonly ILogger<ThreeMiddleware> logger;

        public ThreeMiddleware(ILogger<ThreeMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            logger.LogWarning("Three Middleware");
            await next(context);
        }
    }
}
