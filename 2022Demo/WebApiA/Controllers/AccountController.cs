using Lee.Utility.Config;
using Microsoft.AspNetCore.Http;
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

        [HttpGet(Name = "a")]
        public IActionResult Get()
        {
            var CurrentMonth = _systemContract.GetCurrentMonth();
            var CurrentID = _systemContract.GetCurrentID();


            var CurrentMonth2 = _systemContract2.GetCurrentMonth();
            var CurrentID2 = _systemContract2.GetCurrentID();

            return Ok(new { CurrentMonth, CurrentID });
        }
 
    }
}
