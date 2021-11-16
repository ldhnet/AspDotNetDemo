using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class AdminSafeListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AdminSafeListMiddleware> logger;
        private readonly string _safelist;

        public AdminSafeListMiddleware(
            RequestDelegate next,
            ILogger<AdminSafeListMiddleware> _logger,
            string safelist)
        {
            _safelist = safelist;
            _next = next;
            logger = _logger;
            ;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("WebApi:Ip 安全验证中间件");

            if (context.Request.Method == HttpMethod.Get.Method)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                logger.LogDebug("Request from Remote IP address: {RemoteIp}", remoteIp);

                string[] ip = _safelist.Split(';');

                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;
                foreach (var address in ip)
                {
                    if (string.IsNullOrEmpty(address)) continue;
                    var testIp = IPAddress.Parse(address);
                    if (testIp.GetAddressBytes().SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    logger.LogWarning("Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
