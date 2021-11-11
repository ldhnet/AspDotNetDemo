using Framework.Utility.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks; 

namespace WebMVC.Middleware
{
    [MiddlewareRegister(Sort = 100)]
    public class OneMiddleware : IMiddleware
    {
        private readonly ILogger<OneMiddleware> logger;

        public OneMiddleware(ILogger<OneMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            logger.LogWarning("One Middleware");
            await next(context);
        }
    }
}
