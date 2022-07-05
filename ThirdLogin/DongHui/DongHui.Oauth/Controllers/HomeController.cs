using DongHui.Oauth.Models;
using DongHui.OAuth.Gitee;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace DongHui.Oauth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string userInfo = HttpContext.Session.GetString("OAuthUser");
            if (!string.IsNullOrEmpty(userInfo))
            {
                _logger.LogInformation($"授权者信息==：{userInfo}");
                var result = JsonSerializer.Deserialize<GiteeUserModel>(userInfo);
                ViewBag.OAuthUser = result;
                ViewBag.UserDetail = userInfo;
                return View();
            }
            else
            { 
                return  RedirectToAction("Error");
            }
       
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("OAuthUser");
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}