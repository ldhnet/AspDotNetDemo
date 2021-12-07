using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebApi6_0.Middleware
{
    public class CalculateExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;//下一个中间件
        private readonly ILogger<CalculateExecutionTimeMiddleware> _logger;
        Stopwatch stopwatch;

        public CalculateExecutionTimeMiddleware(RequestDelegate next, ILogger<CalculateExecutionTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();//在下一个中间价处理前，启动计时器

            _logger.LogInformation("访问者 Ip:" + context.Connection.RemoteIpAddress.ToString());

            await _next.Invoke(context);

            stopwatch.Stop();//所有的中间件处理完后，停止秒表。
            _logger.LogInformation($@"接口{context.Request.Path}耗时{stopwatch.ElapsedMilliseconds}ms");
        }
    }
    
}
