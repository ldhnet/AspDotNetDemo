using Lee.Utility.Config;
using Lee.Utility.ViewModels; 
using Microsoft.AspNetCore.Mvc; 
using WebA.Admin.Contracts;

namespace WebApiA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IServiceProvider _provider;
        public AccountController(ILogger<AccountController> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        } 
        private ISystemContract _systemContract => _provider.GetRequiredService<ISystemContract>();
        private ISystemContract _systemContract2 => GlobalConfig.ServiceProvider.GetRequiredService<ISystemContract>();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "a")]
        public IActionResult Get()
        {
            var CurrentMonth = _systemContract.GetCurrentMonth();
            var CurrentID = _systemContract.GetCurrentID();


            var CurrentMonth2 = _systemContract2.GetCurrentMonth();
            var CurrentID2 = _systemContract2.GetCurrentID();

            return Ok(new { CurrentMonth, CurrentID });
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
