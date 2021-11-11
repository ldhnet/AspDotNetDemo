using System; 
using System.Linq;
using System.Threading; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebMVC.Attributes;
using WebMVC.Models;
using WebMVC.Helper; 
using System.Globalization;  
using System.Security.Claims; 
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Framework.Utility.Extensions;
using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using DirectService.Admin.Impl;
using Microsoft.Extensions.DependencyInjection;
using Framework.Utility.Attributes;

namespace WebMVC.Controllers
{
    /// <summary>
    /// 自定义控制器的基类
    /// </summary>
    public class BaseController : Controller
    { 
        /// <summary>
        /// 是否需要登入验证
        /// </summary>
        protected bool IsNeedLogin = true;

        /// <summary>
        /// 在Action执行前触发（如果继承该类的子类也重写了该方法，则先执行子类的方法，再执行父类的方法）
        /// </summary>
        /// <param name="context">Action执行前上下文对象</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            Request.Cookies.TryGetValue("lang", out string lang);

            //获取前台语言并设置
            SetCulture(lang); 

            #region 【权限验证】【登入验证】 

            var IsExistArea = context.ActionDescriptor.RouteValues.TryGetValue("area", out string _areaName);
            if (IsExistArea)
            {
                var areaName = context.ActionDescriptor.RouteValues["area"] ?? "";
                areaName = (string)context.RouteData.Values["area"];
            }
  
            
            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];

            #region 如果是HOME或者CusError控制器忽略，其他需要判断来源

            if (controllerName == "Account") return;

            //获取请求进来的控制器与Action
            var controllerActionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

            //判断当前所请求的Action上是否有打上指定的特性标签
            if (controllerActionDescriptor.ControllerTypeInfo.IsDefined(typeof(SkipLoginValidateAttribute), false))
            {
                return;
            }
            if (controllerActionDescriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false))
            {
                return;
            }
     
            #endregion 如果是HOME或者CusError控制器忽略，其他需要判断来源

            if (CurrentUser == null)
            {
                context.Result = RedirectToAction("Index", "Account", new { area = "" });
                return;
            } 
            #endregion 【权限验证】【登入验证】

            //【权限验证】【登入验证】逻辑 
            //验证用户是否有权限执行Action
            if (CheckPermission(controllerName, actionName))
            {
                return;
            }
            if (context.HttpContext.Request.IsAjax())
                context.Result = Json(new { result = "No Permission" });
            else
                context.Result = RedirectToAction("Error");
        }

     
        /// <summary>
        /// 在Action执行后触发（如果继承该类的子类也重写了该方法，则先执行子类的方法，再执行父类的方法）
        /// 
        /// </summary>
        /// <param name="context">Action执行后上下文对象</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (!(context.Result is ViewResult)) return;

            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var actionName = context.ActionDescriptor.RouteValues["action"];

            //获取当前程序集版本号
            ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
             
            ViewBag.UserCacheModel = JsonHelper.FromJson<UserCacheModel>(HttpContext.Session.GetString(WebConstant.SessionKey.UserCacheModel));

            if (controllerName == "Home" || controllerName == "CusError") return; 
        }
 
        /// <summary>
        /// 登录用户信息
        /// </summary>
        private UserCacheModel _currentUser;

        protected UserCacheModel CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;
                _currentUser = SessionHelper.GetSession<UserCacheModel>(WebConstant.SessionKey.UserCacheModel) ?? JsonHelper.FromJson<UserCacheModel>(HttpContext.Session.GetString(WebConstant.SessionKey.UserCacheModel));

                //var teet= HttpContext.User.Identity.Name.ToUpper();

                var _number = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
                var _name = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if ((_currentUser == null && _number != null) || (_currentUser != null && _currentUser.EmployeeSerialNumber != _number))
                {
                    var services = new ServiceCollection();
                    services.AddSingleton<IUserService, UserService>(); 
                    var provider = services.BuildServiceProvider(); 
                    var userService = provider.GetService<IUserService>();

                    var result = userService.Find(_number);
                    if (result != null)
                    {
                        _currentUser = LoadCurrentUser(result);
                    }
                }
                return _currentUser;
            }
        }
        protected UserCacheModel LoadCurrentUser(Employee model)
        {
            if (model == null)
                return new UserCacheModel();

            //var loginInfo = model.LoginInfo; 

            var currentUser = new UserCacheModel
            {
                EmployeeId = model.Id,
                EmployeeSerialNumber = model.EmployeeSerialNumber,
                EmployeeName = model.Name,
                EnglishName = model.Name,
                OrgId = model.Department ?? 0,
                LoginTime = DateTime.Now,
                PortraitFileName = "/images/headportrait.jpg",
            };

            //SessionHelper.SetSession(WebConstant.SessionKey.UserCacheModel, currentUser);
             
            HttpContext.Session.SetObjectAsJson(WebConstant.SessionKey.UserCacheModel, currentUser);
             
            return currentUser;
        }
        /// <summary>
        /// 设置全局语言区域
        /// </summary>
        /// <param name="lang"></param>
        protected virtual void SetCulture(string lang)
        {
            CultureInfo culture;
            try
            {
                culture = new CultureInfo(lang);
            }
            catch
            {
                culture = new CultureInfo("zh-CN");
            }
            ViewBag.Culture = culture.Name;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// 判断是否有权限访问action
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected bool CheckPermission(string controller, string action)
        {
            return true;
        }


        #region 设置cookies

        //SetCookies("cookieKay", "11111条数据的内容1111");
        //var bb = GetCookies("cookieKay");

        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        protected void SetCookies(string key, string value, int minutes = 30)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

        #endregion
    }
}
