using Lee.Utility.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using Web6demo.Models;

namespace Web6demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private DistributedRedisCache _Cache;
        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _Cache = new DistributedRedisCache(cache);
        }

        public IActionResult Index()
        {
            string val = HttpContext.Session.GetString("test_username"); 
            ViewBag.sessionid = HttpContext.Session.Id;
            ViewBag.val = val;
            return View();
        }

        public IActionResult Privacy()
        { 
            //验证
            bool boolExists = _Cache.Exist("id");
            //获取
            object obj = _Cache.Get("id");

            ViewBag.Id = obj;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}