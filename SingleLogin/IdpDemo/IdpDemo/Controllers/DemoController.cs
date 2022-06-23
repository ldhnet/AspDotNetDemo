using IdpDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdpDemo.Controllers
{
    public class DemoController : Controller
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var aaaaa = Environment.CurrentManagedThreadId;
            var aaa= MyEnv.Version;
            var bbb = MyEnv.GetEnvironmentVariable(Environment.MachineName);
            var bbbw = MyEnv.GetEnvironmentVariables()["ASPNETCORE_ENVIRONMENT"];
            Directory.GetCurrentDirectory();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
          
    }
}