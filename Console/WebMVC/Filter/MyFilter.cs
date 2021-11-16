using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

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
            #region

            //业务逻辑
            //测试测试测试测试测试测试测试测试
            #endregion


            base.OnActionExecuting(context);
        }
    }
}

