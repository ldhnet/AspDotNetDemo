using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcClient_2.Models;
using System.Diagnostics;

namespace MvcClient_2.Controllers
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
            ViewBag.IsLogin = HttpContext.User.Identity?.IsAuthenticated.ToString();
             
            string val = HttpContext.Session.GetString("test_username");
            //session为空写入一条测试数据
            if (string.IsNullOrEmpty(val))
            {
                HttpContext.Session.SetString("test_username", "a");
            }

            ViewBag.sessionid = HttpContext.Session.Id;
            ViewBag.val = val;

            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}