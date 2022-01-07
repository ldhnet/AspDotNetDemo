using DH.Models.Entities;
using DirectService.Admin.Contracts;
using DirectService.Test.Contracts;
using Framework.Utility;
using Framework.Utility.Config;
using Framework.Utility.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi6_0.Filter;
using WebApi6_0.Models.InputDto;

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RedisDemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger; 
        private readonly IUserService _UserInterface;
        private readonly IEmailSender _EmailInterface;
        public RedisDemoController(ILogger<DemoController> logger,IUserService userIfc, IEmailSender emailface)
        {
            _logger = logger; 
            _UserInterface = userIfc;
            _EmailInterface = emailface;            
        }
        /// <summary>
        /// 测试Demo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString); 
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");
               
            var result = _UserInterface.Find("adminaaa")!;

            var aaaa= HttpContext.User;

            var list = _UserInterface.GetAllList();

            CheckParameter.NotNull(list, "adminaaa");
            //CheckParameter.Required(result,c => c != null, "dto 不能为空");

            return Ok(list);
        }

        /// <summary>
        /// Post测试Demo
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]DemoDto demoDto)
        { 
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222"); 

            _logger.LogError("3333333");


            string url = $"baidu.com";
            string body =
                $"亲爱的用户 <strong>{demoDto.Name}</strong>[{demoDto.Name}]，您好！<br>"
                + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                + $"{url}<br>"
                + $"祝您使用愉快！";
            await _EmailInterface.SendEmailAsync("2283259182@qq.com", "测试邮件", body);
              
            var data = new
            {
                demoInfo = demoDto,
                deomList = new List<DemoDto>() { demoDto, demoDto },
                message = "测试"
            }; 
            return Ok(new BaseResponse<dynamic>(successCode.Success, string.Empty, data));
        }
    }
}
