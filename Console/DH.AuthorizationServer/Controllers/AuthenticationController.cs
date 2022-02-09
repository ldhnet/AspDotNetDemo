using DH.AuthorizationServer.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DH.AuthorizationServer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICustomJWTService _iJWTService;
        public AuthenticationController(ILogger<AuthenticationController> logger, ICustomJWTService jwtService)
        {
            _logger = logger;
            _iJWTService = jwtService; 
        }
        public IActionResult Index()
        {
            return View();
        }
        private void ThreadMethod()
        { 
            Process processes = Process.GetCurrentProcess();
            _logger.LogInformation($"进程ID={processes.Id}     线程ID={Thread.CurrentThread.ManagedThreadId.ToString()}");
            Thread.Sleep(60 * 60 * 60);
        }
        [Route("Login")]
        [HttpPost]
        public string Login(string name,string password)
        {
            {
                //do 数据库校验
                for (int i = 0; i < 1000; i++)
                {
                    Thread th = new Thread(new ThreadStart(ThreadMethod)); //创建线程                     
                    th.Start(); //启动线程  
                }

            }
            if ("admin".Equals(name) && "123456".Equals(password))
            {
                string token = this._iJWTService.GetToken(name, password);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token,
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token="",
                });
            }
        }
    }
}
