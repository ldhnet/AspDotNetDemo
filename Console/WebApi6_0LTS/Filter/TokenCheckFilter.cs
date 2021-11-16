using Framework.Utility;
using Framework.Utility.Extensions;
using Framework.Utility.Helper;
using Framework.Utility.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters; 
namespace WebApi6_0.Filter
{
    /// <summary>
    /// token验证
    /// </summary>
    public class TokenCheckFilter : AuthorizeFilter
    {
        private static AuthorizationPolicy _policy_ = new AuthorizationPolicy(new[] { new DenyAnonymousAuthorizationRequirement() }, new string[] { });
        /// <summary>
        /// 构造函数
        /// </summary>
        public TokenCheckFilter() : base(_policy_) { }
 
        /// <summary>
        /// 重写基类的验证方式，加入自定义的Ticket验证
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //解密Ticket
            var ApiToken = SecurityHelper.Base64Encrypt("admin&123456");
            await base.OnAuthorizationAsync(context);
            string token = context.HttpContext.Request.Headers["ApiToken"].ParseToString();

            //获取请求进来的控制器与Action
            var controllerActionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

            if (controllerActionDescriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }  
            if (!string.IsNullOrEmpty(token.ToString()))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                if (ValidateTicket(token.ToString()))
                {
                    return;
                }
                else
                {
                    BaseResponse obj = new BaseResponse(successCode.Fail);
                    obj.msg = "抱歉，没有登录或登录已超时";
                    context.Result = new JsonResult(obj);
                    return;
                };
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证403
            else
            {
                BaseResponse obj = new BaseResponse(successCode.Fail);
                obj.msg = "抱歉，没有登录或登录已超时";
                context.Result = new JsonResult(obj);
                return;
            }
          
        } 

        //校验用户名密码（对Session匹配，或数据库数据匹配）
        private bool ValidateTicket(string encryptToken)
        {
            //解密Ticket
            var strTicket = SecurityHelper.Base64Decrypt(encryptToken);
            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string userName = strTicket.Substring(0, index);
            string password = strTicket.Substring(index + 1);
            //dosomeing
            if (userName == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        } 


    }
}