using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMVC.Attributes;

namespace WebMVC.Middleware
{  
    public class ResponseHeaderMiddleware : IMiddleware
    {
        private readonly ILogger<ResponseHeaderMiddleware> logger;

        public ResponseHeaderMiddleware(ILogger<ResponseHeaderMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            logger.LogWarning("response header Middleware 这个例子只是修改一下response的header");

            //这个例子只是修改一下response的header
            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("lang", "zh-CH");
                return Task.FromResult(0);
            }, context);
            await next(context);
        }
         
    }
}
