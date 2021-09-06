using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Filter
{
    public class MyFilter : ActionFilterAttribute
    {
        private readonly ILogger<MyFilter> _logger;

        public MyFilter(ILogger<MyFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("do2222 something.....");
            base.OnActionExecuting(context);
        }
    }
}
