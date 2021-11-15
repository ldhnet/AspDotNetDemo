using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Core.Data;
using Framework.Utility;
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
        private readonly IUnitOfWork _unitOfWork;
        public DBTestController(ILogger<DBTestController> logger, IUserService userService, IUnitOfWork unitOfWork, ISysAccountContract sysAccountContract)
        {
            _logger = logger;
            _SysAccountContract = sysAccountContract;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet] 
        public string Get()
        { 

            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString);


            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");




            var BeginTransactionTest = _SysAccountContract.BeginTransactionTest();


            SysAccount model = new SysAccount
            {
                UserId = Guid.NewGuid().ToString(),
                AccountName = "admin",
                AccountNo = "1001",
                CreateBy = "admin", 
                CreateTime = DateTime.Now
            };


            Employee model2 = new Employee
            {
                Name = "admin1" + new Random().Next(1),
                BankCard = "admin1",
                EmployeeName = "1001",
                Department = 1,
                Phone = "15225074031",
                EmployeeSerialNumber = "1001",
            };


            //var aaa3 = _SysAccountContract.CreateInfo(model);

            //var aaa5 = _userService.CreateInfo(model2);


          
            var aaa2 = _SysAccountContract.GetSysAccount("admin1");


            var aaa6 = _userService.GetAll().ToList();




            var aaa = _SysAccountContract.GetSysAccountInfo("admin");
            //return $"{aaa?.AccountName}{aaa?.AccountNo}";

            return $"{aaa?.AccountName}{aaa?.AccountNo},{aaa2.data.AccountName}{aaa2.data.AccountNo}";
        }
    }
}
