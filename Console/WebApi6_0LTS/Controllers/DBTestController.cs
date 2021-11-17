using AutoMapper;
using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using DirectService.Test;
using Framework.Auth;
using Framework.Core.Data;
using Framework.Utility.Config;
using Framework.Utility.Extensions;
using Framework.Utility.Helper;
using Framework.Utility.Mapping;
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

            var model2 = new Foo()
            {
                Id = 1,
                Name = "test",
                Money = 15.0m,
                CreateBy ="ldh"
            };
            //var aaadto= Mapper.Map<FooDto>(model2);

            var aaa2o = model2.MapTo<FooDto>();


            var emp = new DataRepository().GetUserByToken(ApiToken) ?? new OperatorInfo();

            //var dto = Mapper.Map<Employee>(emp);

           //var BeginTransactionTest = _SysAccountContract.BeginTransactionTest();
 
             
            var aaa2 = _SysAccountContract.GetSysAccount("admin1");
             
            var aaa6 = _userService.GetAll().ToList();

             
            var aaa = _SysAccountContract.GetSysAccountInfo("admin");
            //return $"{aaa?.AccountName}{aaa?.AccountNo}";

            return $"{aaa?.AccountName}{aaa?.AccountNo},{aaa2.data.AccountName}{aaa2.data.AccountNo}";
        }
    }
}
