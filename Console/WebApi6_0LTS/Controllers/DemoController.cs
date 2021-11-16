using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using DirectService.Test.Contracts;
using Framework.Utility;
using Framework.Utility.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi6_0.Filter;
using WebApi6_0.Models.InputDto;

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private readonly ITestInterface _TestInterface;
        private readonly IUserService _UserInterface;
        public DemoController(ILogger<DemoController> logger, ITestInterface testIfc, IUserService userIfc)
        {
            _logger = logger;
            _TestInterface = testIfc;
            _UserInterface = userIfc;
        }
        /// <summary>
        /// 测试Demo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        //[AuthorizeFilter]
        [AllowAnonymous]
        public BaseResponse<Employee> Get()
        {
            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString); 
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");

            var aaa = _TestInterface.TestFun();

            _logger.LogError(aaa);

            var result = _UserInterface.Find("admin"); 
            return new BaseResponse<Employee>(successCode.Success,"",result);
        }

        /// <summary>
        /// Post测试Demo
        /// </summary>
        /// <returns></returns>  
        [HttpPost] 
        public BaseResponse<dynamic> Post([FromBody]DemoDto demoDto)
        { 
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222"); 

            _logger.LogError("3333333");

            var data = new
            {
                demoInfo = demoDto,
                deomList = new List<DemoDto>() { demoDto, demoDto },
                message = "测试"
            }; 
            return new BaseResponse<dynamic>(successCode.Success, string.Empty, data);
        }
    }
}
