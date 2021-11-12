using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;   
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebMVC.Model;
using WebMVC.Extension;
using WebMVC.Helper;
using Framework.Utility.Extensions;
using Framework.Utility.Helper;

namespace WebMVC.Filter
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            NLogHelper.Error(context.Exception);
            if (context.HttpContext.Request.IsAjax())
            {
                TData obj = new TData();
                obj.Message = context.Exception.GetOriginalException().Message;
                if (string.IsNullOrEmpty(obj.Message))
                {
                    obj.Message = "抱歉，系统错误，请联系管理员！";
                }
                context.Result = new JsonResult(obj);
                context.ExceptionHandled = true;
            }
            else
            {
                string errorMessage = context.Exception.GetOriginalException().Message;
                context.Result = new RedirectResult("~/Home/Error?message=" + HttpUtility.UrlEncode(errorMessage));
                context.ExceptionHandled = true;
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}
