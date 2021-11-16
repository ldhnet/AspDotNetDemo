using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class OtherMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<OtherMiddleware> logger;
        public OtherMiddleware(RequestDelegate next, ILogger<OtherMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("WebApi:这个例子只是修改一下response的header");



            //这个例子只是修改一下response的header
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("test1response", "responseheader");
                return Task.FromResult(0);
            }, context);
            await next(context);
        }

    }
}
