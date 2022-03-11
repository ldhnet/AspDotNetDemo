using Microsoft.AspNetCore.Mvc;
using WebApi6_0.Models;

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger; 
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// swagger登录
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("swgLogin")]
        public dynamic SwgLogin([FromBody] SwaggerLoginRequest loginRequest)
        {
            _logger.LogInformation("api/Login/swgLogin");
            // 这里可以查询数据库等各种校验
            if (loginRequest?.name == "admin" && loginRequest?.pwd == "admin")
            {
                HttpContext.Session.SetString("swagger-code", "success");
               
                return new { result = true };
            } 
            return new { result = false };
        }
    }
}
