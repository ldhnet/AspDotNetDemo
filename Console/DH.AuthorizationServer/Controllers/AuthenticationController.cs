using DH.AuthorizationServer.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [Route("Login")]
        [HttpPost]
        public string Login(string name,string password)
        {
            { 
                //do 数据库校验
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
