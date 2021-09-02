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
        /// 在Action执行前触发（如果继承该类的子类也重写了该方法，则先执行子类的方法，再执行父类的方法）
        /// </summary>
        /// <param name="context">Action执行前上下文对象</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context); 

            //获取请求进来的控制器与Action
            var controllerActionDescriptor =
                context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

            #region 【权限验证】【登入验证】 

            var IsExistArea = context.ActionDescriptor.RouteValues.TryGetValue("area", out string _areaName);
            if (IsExistArea)
            {
                var areaName = context.ActionDescriptor.RouteValues["area"] ?? "";
                areaName = (string)context.RouteData.Values["area"];
            }  
            var controllerName = context.ActionDescriptor.RouteValues["controller"];       
            var actionName = context.ActionDescriptor.RouteValues["action"]; 
            
            #region 如果是HOME或者CusError控制器忽略，其他需要判断来源

            if (controllerName == "Account") return;

            #endregion 如果是HOME或者CusError控制器忽略，其他需要判断来源

            if (CurrentUser == null)
            {
                context.Result = RedirectToAction("Index", "Account", new { area = "" });
                return;
            }
      
            //判断当前所请求的Action上是否有打上指定的特性标签
            if (controllerActionDescriptor.MethodInfo.IsDefined(typeof(SkipLoginValidateAttribute), false) || !IsNeedLogin)
            { 
                return;
            }

            #endregion 【权限验证】【登入验证】

            //【权限验证】【登入验证】逻辑 
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
