using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using WebMVC.Attributes;
using WebMVC.Models;
using WebMVC.Helper;
using System.Diagnostics.Contracts;
using System.Globalization;
using WebMVC.Model;
using WebMVC.Service;
using static System.Collections.Specialized.BitVector32;

namespace WebMVC.Controllers
{
    /// <summary>
    /// 自定义控制器的基类
    /// </summary>
    public class BaseController : Controller
    {
        private IUserService userService = new UserService();
        /// <summary>
        /// 是否需要登入验证
        /// </summary>
        protected bool IsNeedLogin = true;

        /// <summary>
        /// 在Action执行前触发（用于让子类重写）
        /// </summary>
        /// <param name="context">Action执行前上下文对象</param>
        protected virtual void OnSubActionExecuting(ActionExecutingContext context)
        {

        }

        /// <summary>
        /// 在Action执行前触发（如果继承该类的子类也重写了该方法，则先执行子类的方法，再执行父类的方法）
        /// </summary>
        /// <param name="context">Action执行前上下文对象</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            OnSubActionExecuting(context); //先执行子类的

            //获取请求进来的控制器与Action
            var controllerActionDescriptor =
                context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

            #region 【权限验证】【登入验证】

            //获取区域名称
            var IsExistArea = context.ActionDescriptor.RouteValues.TryGetValue("area", out string _areaName);
            if (IsExistArea)
            {
                var areaName = context.ActionDescriptor.RouteValues["area"] ?? "";
                areaName = (string)context.RouteData.Values["area"];
            }
             
            //获取控制器名称
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            //controllerName = controllerActionDescriptor.ControllerName;
            //controllerName = context.RouteData.Values["controller"].ToString();

            //获取action名称
            var actionName = context.ActionDescriptor.RouteValues["action"];
            //actionName = controllerActionDescriptor.ActionName;
            //actionName = context.RouteData.Values["action"].ToString();

            ////获取当前的请求方式：Get或者Post
            //string requestMethod = context.HttpContext.Request.Method.ToLower();

            ////获取路由占位符对应的值
            //object currId = context.RouteData.Values["id"];

            //获取当前请求URL参数
            var value =  context.HttpContext.Request.Query.ContainsKey("key") ? context.HttpContext.Request.Query["key"].ToString() : "";

             
            #region 如果是HOME或者CusError控制器忽略，其他需要判断来源

            if (controllerName == "Login" || controllerName == "Home" || controllerName == "CusError") return;

            #endregion 如果是HOME或者CusError控制器忽略，其他需要判断来源

            if (CurrentUser != null)
            {
                context.Result = RedirectToAction("Index", "Login", new { area = "" });
                return;
            }
     
          
            //判断当前所请求的控制器上是否有打上指定的特性标签
            if (controllerActionDescriptor.ControllerTypeInfo.IsDefined(typeof(SkipLoginValidateAttribute), false))
            {
                //有的话就不进行相关操作【例如：不进行登入验证】
                //return;
            }

            //获取当前所请求的Action上指定的特性标签
            object[] arrObj1 = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(SkipLoginValidateAttribute), false);
            //获取当前所请求的Action上所有的特性标签
            object[] arrObj2 = controllerActionDescriptor.MethodInfo.GetCustomAttributes(false);

            //判断当前所请求的Action上是否有打上指定的特性标签
            if (controllerActionDescriptor.MethodInfo.IsDefined(typeof(SkipLoginValidateAttribute), false) || !IsNeedLogin)
            {
                //有的话就不进行相关操作【例如：不进行登入验证】
                return;
            }

            #endregion 【权限验证】【登入验证】

            //【权限验证】【登入验证】逻辑
            //To Do Something

            //验证用户是否有权限执行Action
            if (CheckPermission(controllerName, actionName))
            {
                return;
            }
        }
         

        /// <summary>
        /// 在Action执行后触发（如果继承该类的子类也重写了该方法，则先执行子类的方法，再执行父类的方法）
        /// </summary>
        /// <param name="context">Action执行后上下文对象</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
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
                _currentUser = SessionHelper.GetSession<UserCacheModel>("UserCacheModel");
                if (_currentUser == null && User.Identity.IsAuthenticated || _currentUser != null && _currentUser.EmployeeSerialNumber != User.Identity.Name)
                {
                    var result = userService.Find(User.Identity.Name);
                    if (result != null)
                    {
                        _currentUser = LoadCurrentUser(result); 
                        SessionHelper.SetSession("UserCacheModel", _currentUser); 
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
                EmployeeName = model.EmployeeName,
                EnglishName = model.EmployeeName,
                OrgId = model.Department,
                LoginTime = DateTime.Now, 
                PortraitFileName = "/images/headportrait.jpg", 
            };
            return currentUser;
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
    }
}
