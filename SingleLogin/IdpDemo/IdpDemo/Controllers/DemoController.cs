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
            var aaaaa = System.Environment.CurrentManagedThreadId;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}