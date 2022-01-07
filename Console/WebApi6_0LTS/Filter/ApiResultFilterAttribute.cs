using Framework.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi6_0.Filter
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// api参数校验
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
        /// <summary>
        /// api请求成功，数据异常返回值处理
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ValidationFailedResult)
            {
                var objectResult = context.Result as ObjectResult;
                context.Result = objectResult!;
            }
            else if(context.Result is BadRequestObjectResult)
            {
                var objectResult = context.Result as ObjectResult; 
                context.Result = new OkObjectResult(new TResponse<object>() {Success=0, Code = StatusCodes.Status400BadRequest, Data = objectResult?.Value! });
            }
        }
    }
}
