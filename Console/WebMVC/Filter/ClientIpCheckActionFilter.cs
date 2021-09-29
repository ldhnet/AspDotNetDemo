using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using WebMVC.Attributes;
using Microsoft.AspNetCore.Authorization;
using WebMVC.Extension;

namespace WebMVC.Filter
{

    public class ClientIpCheckActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger; 

        public ClientIpCheckActionFilter(ILogger logger)
        { 
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                if (controllerActionDescriptor.ControllerTypeInfo.CustomAttributes.IsContainAttribute(typeof(AllowAnonymousAttribute))) return;
                if (controllerActionDescriptor.MethodInfo.CustomAttributes.IsContainAttribute(typeof(AllowAnonymousAttribute))) return; 
            }


            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            { 
                return;
            } 
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            _logger.LogDebug("Remote IpAddress: {RemoteIp}", remoteIp);

            string _safeString = "127.0.0.1;192.168.1.5;::1";
            var ip = _safeString.Split(';');
            var badIp = true;

            if (remoteIp.IsIPv4MappedToIPv6)
            {
                remoteIp = remoteIp.MapToIPv4();
            }

            foreach (var address in ip)
            {
                var testIp = IPAddress.Parse(address);

                if (testIp.Equals(remoteIp))
                {
                    badIp = false;
                    break;
                }
            }

            if (badIp)
            {
                _logger.LogWarning("Forbidden Request from IP: {RemoteIp}", remoteIp);
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }



    }
}
