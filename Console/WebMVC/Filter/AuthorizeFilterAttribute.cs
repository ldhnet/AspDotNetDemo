using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Extension;
using WebMVC.Helper;
using WebMVC.Model;

namespace WebMVC.Filter
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public AuthorizeFilterAttribute() { }

        public AuthorizeFilterAttribute(string authorize)
        {
            this.Authorize = authorize;
        }

        /// <summary>
        /// 权限字符串，例如 organization:user:view
        /// </summary>
        public string Authorize { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            bool hasPermission = false;

            var aaaa = context.HttpContext.User.Identity.IsAuthenticated;

            var bbb = context.Filters.Any(item => item is IAllowAnonymousFilter);


            if (1 == 0)
            {
                // 防止用户选择记住我，页面一直在首页刷新
                if (new CookiesHelper().GetCookie("RememberMe").ParseToInt() == 1)
                { 
                }

                #region 没有登录
                if (context.HttpContext.Request.IsAjax())
                {
                    TData obj = new TData();
                    obj.Message = "抱歉，没有登录或登录已超时";
                    context.Result = new JsonResult(obj);
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("~/Home/Login");
                    return;
                }
                #endregion
            }
            else
            {
                // 系统用户拥有所有权限
                if (1 == 1)
                {
                    hasPermission = true;
                }
                else
                {
                    // 权限判断
                    if (!string.IsNullOrEmpty(Authorize))
                    {
                        string[] authorizeList = Authorize.Split(','); 
                       
                        if (!hasPermission)
                        {
                            if (context.HttpContext.Request.IsAjax())
                            {
                                TData obj = new TData();
                                obj.Message = "抱歉，没有权限";
                                context.Result = new JsonResult(obj);
                            }
                            else
                            {
                                context.Result = new RedirectResult("~/Home/NoPermission");
                            }
                        }
                    }
                    else
                    {
                        hasPermission = true;
                    }
                }
                if (hasPermission)
                {
                    var resultContext = await next();
                }
            }
        }
    }
}
