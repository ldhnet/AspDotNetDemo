using Framework.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi6_0.Filter
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ValidationFailedResult)
            {
                var objectResult = context.Result as ObjectResult;
                context.Result = objectResult!;
            }
            //else
            //{
            //    var objectResult = context.Result as ObjectResult;
            //    context.Result = new OkObjectResult(new TResponse<object>() { HttpCode = 200, Data = objectResult?.Value! });
            //}
        }
    }
}
