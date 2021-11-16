using DirectService.Admin.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using WebMVC.Helper;
using WebMVC.Model;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
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
        [HttpGet]
        public IActionResult LoginTest(int id)
        {
            return Json(new { id = id });
        }
        [HttpPost]
        public JsonResult Login(LoginViewModel model)//[NotNull] 
        {
            TData obj = new TData();
            var name = (model?.account ?? string.Empty).ToLower();
            var pwd = model.password;
            var employee = _userService.FindEmployee(name);
            if (employee.data == null)
            {

                obj.Tag = 2;
                obj.Message = "用户名密码不正确";
                return Json(obj);
            }
            var claims = new[] {
                    new Claim(ClaimTypes.Email, "574427343@qq.com"),
                    new Claim(ClaimTypes.SerialNumber,employee.data.EmployeeSerialNumber),
                    new Claim(ClaimTypes.Name,employee.data.Name),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);

            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user).Wait();
            //可以使用HttpContext.SignInAsync方法的重载来定义持久化cookie存储用户认证信息，例如下面的代码就定义了用户登录后60分钟内cookie都会保留在客户端计算机硬盘上，
            //即便用户关闭了浏览器，60分钟内再次访问站点仍然是处于登录状态，除非调用Logout方法注销登录。 
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties() { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddMinutes(60) }).Wait();

            LoadCurrentUser(employee.data);

            obj.Tag = 1;
            obj.Message = employee.msg;

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
