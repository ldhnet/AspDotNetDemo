using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApiB.Code;

namespace WebApiB.Controllers
{
    [ApiController] 
    public class DemoController : ControllerBase
    {
        
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Demo/Getlist")]
        public IActionResult Get()
        {
             return Ok("hello");
        }
        [HttpGet]
        [Route("Demo/getTest")]
        public IActionResult Test(string email, string body)
        {
 
            var mailInfo = new Sys_MailInfo
            {
                To = email,
                Subject = "XXXXXX֪ͨ(ϵͳ�ʼ�)",
                Body = body, 
            };
            mailInfo.AddMailToQueue();
             
            return Ok(1);
        }

  
    }
}