using AutoMapper;
using DH.Models.DbModels;
using DirectService.Admin;
using DirectService.Admin.Contracts;
using DirectService.Admin.Dto;
using DirectService.Test;
using Framework.Auth;
using Framework.Core.Data;
using Framework.Utility;
using Framework.Utility.Config;
using Framework.Utility.Extensions;
using Framework.Utility.Helper;
using Framework.Utility.Mapping;
using Framework.Utility.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string Get()
        {

            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString);
             
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");

            string ApiToken = "a5f3d50ab2084821953d4d45925a042a";


            var aaa10 = _userService.UpdateEmployee(new EmployeeDto());

            var aaa9 = _userService.UpdateEmployee(new Employee());
             
            var aaa8 = _userService.Find("1001");

            //var aaa9 = aaa8.EmployeeDetail;
            //var aaa10 = aaa8.EmployeeLogins; 

            var aaa77 = _userService.GetAllList().ToList();


            var aaa775 = aaa77.FirstOrDefault().EmployeeDetail;

            var aaa776 = aaa77.FirstOrDefault().EmployeeLogins;

            var aaa6 = _userService.GetAll().ToList();


            var cce = ShardResource.Hello;

            string key = new AesHelper().Key;
             
            AesHelper aes = new AesHelper();
            string source = "admin";

            NLogHelper.Error(source);

            var aaaaaaaaa = HashHelper.GetMd5(source);
            var aaaaaaaaa2 = HashHelper.GetSha1(source);
              
            var aaass = aes.Encrypt(source);

            var bbbss = aes.Decrypt(aaass);


            var model2 = new Foo()
            {
                Id = 1,
                Name = "test",
                Money = 15.0m,
                CreateBy ="ldh"
            };
            //var aaadto= Mapper.Map<FooDto>(model2);

            var aaa2o = model2.MapTo<FooDto>();


            var emp = new DataRepository().GetUserByToken(ApiToken) ?? new Employee();

            //var dto = Mapper.Map<Employee>(emp);

           var BeginTransactionTest = _SysAccountContract.BeginTransactionTest();
 
             
            var aaa2 = _SysAccountContract.GetSysAccount("admin1");
              
            var aaa = _SysAccountContract.GetSysAccountInfo("admin");
            //return $"{aaa?.AccountName}{aaa?.AccountNo}";

            return $"{aaa?.AccountName}{aaa?.AccountNo},{aaa2.data.AccountName}{aaa2.data.AccountNo}";
        }
    }
}
