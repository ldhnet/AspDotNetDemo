using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMVC.Model;
using WebMVC.Models;
using WebMVC.Helper;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.Extensions.Logging;
using WebMVC.Service;
using Microsoft.AspNetCore.Http;
using WebMVC.Context;
using System.Diagnostics.CodeAnalysis;
using WebMVC.Business;
using Microsoft.AspNetCore.Authorization;

namespace WebMVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private EmoloyeeDLL _userdll = new EmoloyeeDLL(); 
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger; 
        }
        [HttpGet] 
        public IActionResult Index()
        {
            _logger.LogInformation("Index");
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        { 
            return Redirect("~/Account/Index");
        }
        [HttpPost]
        public IActionResult Login([NotNull] LoginViewModel model)
        {
            TData obj = new TData();
            var name = (model?.account??string.Empty).ToLower();
            var pwd = model.password; 
            var employee  = _userdll.Find(name); 
            if (employee.Data == null)
            {

                obj.Tag = 2;
                obj.Message = "用户名密码不正确";
                return Json(obj);
            } 
            var claims = new[] {
                    new Claim(ClaimTypes.Email, "574427343@qq.com"),
                    new Claim(ClaimTypes.SerialNumber,employee.Data.EmployeeSerialNumber),
                    new Claim(ClaimTypes.Name,employee.Data.Name), 
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
 
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user).Wait();
            //可以使用HttpContext.SignInAsync方法的重载来定义持久化cookie存储用户认证信息，例如下面的代码就定义了用户登录后60分钟内cookie都会保留在客户端计算机硬盘上，
            //即便用户关闭了浏览器，60分钟内再次访问站点仍然是处于登录状态，除非调用Logout方法注销登录。 
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties() { IsPersistent = true,  ExpiresUtc = DateTimeOffset.Now.AddMinutes(60) }).Wait();

            LoadCurrentUser(employee.Data);

            obj.Tag = employee.Tag;
            obj.Message = employee.Message;
             
            return Json(obj);
        }


        /// <summary>
        /// 注销登录
        /// </summary> 
        [HttpGet]
        public IActionResult Logout()
        {
            SessionHelper.RemoveSession("UserCacheModel");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Account");
        }
    }
 
}
