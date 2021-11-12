using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Utility.Config; 
using Microsoft.AspNetCore.Mvc;  

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBTestController : ControllerBase
    { 
        private readonly ILogger<DBTestController> _logger;
        private readonly ISysAccountContract _SysAccountContract;
        private readonly IUserService _userService;
        public DBTestController(ILogger<DBTestController> logger, IUserService userService, ISysAccountContract sysAccountContract)
        {
            _logger = logger;
            _SysAccountContract = sysAccountContract;
            _userService = userService;
        }

        [HttpGet] 
        public string Get()
        { 

            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString);


            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");


            SysAccount model = new SysAccount
            {
                UserId = Guid.NewGuid().ToString(),
                AccountName = "admin",
                AccountNo = "1001",
                CreateBy = "admin", 
                CreateTime = DateTime.Now
            };


            SysAccount model2 = new SysAccount
            {
                UserId = Guid.NewGuid().ToString(),
                AccountName = "admin1",
                AccountNo = "1001",
                CreateBy = "admin1",
                CreateTime = DateTime.Now
            };


            //var aaa3 = _SysAccountContract.CreateInfo(model);

            //var aaa5 = _SysAccountContract.CreateInfo(model2);



            var aaa2 = _SysAccountContract.GetSysAccount("admin1");


            var aaa5 = _userService.Find("admin1");


            var aaa = _SysAccountContract.GetSysAccountInfo("admin");
            //return $"{aaa?.AccountName}{aaa?.AccountNo}";

            return $"{aaa?.AccountName}{aaa?.AccountNo},{aaa2.data.AccountName}{aaa2.data.AccountNo}";
        }
    }
}
