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

namespace WebMVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private IUserService userService = new UserService();
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        public object Index()
        {
            _logger.LogInformation("Index");
            return View();
        }

        public IActionResult Login(LoginViewModel model)
        {
            var requestIP = Request.Host;

            var name = model.account;
            var pwd = model.password;
    

            var employee = userService.Find(name);
            LoadCurrentUser(employee);


            var claims = new[] {
                    new Claim(ClaimTypes.Email, employee.Department.ToString()),
                    new Claim(ClaimTypes.SerialNumber,employee.EmployeeSerialNumber),
                    new Claim(ClaimTypes.Name,employee.EmployeeName)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
            //登录用户，相当于ASP.NET中的FormsAuthentication.SetAuthCookie

            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user).Wait();

            //可以使用HttpContext.SignInAsync方法的重载来定义持久化cookie存储用户认证信息，例如下面的代码就定义了用户登录后60分钟内cookie都会保留在客户端计算机硬盘上，
            //即便用户关闭了浏览器，60分钟内再次访问站点仍然是处于登录状态，除非调用Logout方法注销登录。

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties() { IsPersistent = true, ExpiresUtc = DateTimeOffset.Now.AddMinutes(60) }).Wait();

            return RedirectToAction("Index", "Home");
        }
 
        /// <summary>
        /// 注销登录
        /// </summary>
        public IActionResult Logout()
        {
            SessionHelper.RemoveSession("UserCacheModel");
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Account");
        }
    }
 
}
