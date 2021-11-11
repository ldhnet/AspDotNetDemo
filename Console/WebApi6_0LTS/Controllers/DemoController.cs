using DH.Models.DbModels;
using Framework.Utility.Config; 
using Microsoft.AspNetCore.Mvc; 
using WebMVC.Service;

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    { 
        private readonly ILogger<DemoController> _logger;
        private readonly ITestInterface  _TestInterface;
        private readonly IUserService _UserInterface;
        public DemoController(ILogger<DemoController> logger, ITestInterface testIfc, IUserService userIfc)
        {
            _logger = logger;
            _TestInterface = testIfc;
            _UserInterface = userIfc;
        }

        [HttpGet] 
        public string Get()
        {
            MyDBContext ct = new MyDBContext();

            var aa = ct.SysAccount.FirstOrDefault();

            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString);


            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");

            var aaa=  _TestInterface.TestFun();
             
            _logger.LogError(aaa);


           //var bb=  _UserInterface.FindEmployee("admin").Name;
           // _logger.LogError(bb);

            return "123";
        }
    }
}
