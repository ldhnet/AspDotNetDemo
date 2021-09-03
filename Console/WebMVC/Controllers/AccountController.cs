﻿using Microsoft.AspNetCore.Authentication.Cookies;
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

namespace WebMVC.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService= userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Index");
            return View();
        }
   
        [HttpPost]
        public IActionResult Login([NotNull] LoginViewModel model)
        { 
            var name = (model?.account??string.Empty).ToLower();
            var pwd = model.password; 
            var employee  = _userService.Find(name);

            if (employee == null)
            {
                return Json(new OptionResult { resultType = 2, resultMsg = "登录失败", data = "" });
            } 
            var claims = new[] {
                    new Claim(ClaimTypes.Email, "574427343@qq.com"),
                    new Claim(ClaimTypes.SerialNumber,employee.EmployeeSerialNumber),
                    new Claim(ClaimTypes.Name,employee.Name), 
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
 
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user).Wait();
            //可以使用HttpContext.SignInAsync方法的重载来定义持久化cookie存储用户认证信息，例如下面的代码就定义了用户登录后60分钟内cookie都会保留在客户端计算机硬盘上，
            //即便用户关闭了浏览器，60分钟内再次访问站点仍然是处于登录状态，除非调用Logout方法注销登录。 
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties() { IsPersistent = true,  ExpiresUtc = DateTimeOffset.Now.AddMinutes(60) }).Wait();
            LoadCurrentUser(employee);
            return Json(new OptionResult {resultType=1, resultMsg = "登录成功", data = "" });
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
