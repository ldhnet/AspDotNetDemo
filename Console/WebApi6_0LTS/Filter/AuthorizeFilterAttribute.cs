using DirectService.Admin.Contracts;
using Framework.Auth;
using Framework.Utility;
using Framework.Utility.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks; 

namespace WebApi6_0.Filter
{
    /// <summary>
    /// 验证token
    /// </summary>
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public AuthorizeFilterAttribute() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorizeFilterAttribute(string authorize)
        {
            this.Authorize = authorize;
        } 
        /// <summary>
        /// 权限字符串，例如 organization:user:view
        /// </summary>
        public string Authorize { get; set; }
        /// <summary>
        /// 异步验证
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
            bool hasPermission = false; 
            var IsAllowAnonymous = context.Filters.Any(item => item is IAllowAnonymousFilter);
            string token = context.HttpContext.Request.Headers["ApiToken"].ParseToString();
            if (!IsAllowAnonymous)
            {
                OperatorInfo? user = Operator.Instance.Current(token);
                if (user == null || user.UserId == 0)
                {
                    BaseResponse obj = new BaseResponse(successCode.Fail);
                    obj.msg = "抱歉，没有登录或登录已超时";
                    context.Result = new JsonResult(obj);
                    return;
                }
                else
                {
                    hasPermission = true;
                }
            }
            else
            {
                hasPermission = true;
            }

            if (hasPermission)
            {
                var resultContext = await next();
            }
        }
    }
}
