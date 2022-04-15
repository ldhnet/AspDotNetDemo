using Lee.Utility.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics; 
using WebA.Attributes;
using WebA.Models;

namespace WebA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; 
        }

        public IActionResult Index()
        {
         
            var userName = User.Identity.Name;

            var authenticationType = User.Identity.AuthenticationType;

            var authenticate = HttpContext.User.Identity.IsAuthenticated;

            string val = HttpContext.Session.GetString("test_username");
            //session为空写入一条测试数据
            if (string.IsNullOrEmpty(val))
            {
                HttpContext.Session.SetString("test_username", "a1111111111");
            }

            ViewBag.sessionid = HttpContext.Session.Id;
            ViewBag.val = val;

            ViewBag.UserName = userName;
            ViewBag.AuthenticationType = authenticationType;
            ViewBag.IsAuthenticated = authenticate;

            return View();
        } 
        public IActionResult Privacy()
        {
            Console.WriteLine($"**************当前线程Id:{Thread.CurrentThread.ManagedThreadId}*****************");
            TaskDemo taskDemo=new TaskDemo();
            //taskDemo.GetString();

            Task.Run(() =>
            {
                taskDemo.GetStringAsync();
            });
             
            return View();
        }
        [HttpGet]
        public IActionResult PostTestA()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PostTestB()
        {
            return View();
        }
        [HttpPost]
        [PreventDoublePost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //PreventDoublePost Attribute makes ModelState invalid
            }
            return Redirect("/Home/PostTestB");
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}