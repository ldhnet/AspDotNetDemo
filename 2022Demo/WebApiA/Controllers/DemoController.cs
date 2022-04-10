using Microsoft.AspNetCore.Mvc;
using WebA.Admin;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private ServiceContext _context;
        public DemoController(ILogger<DemoController> logger, ServiceContext context)
        {
            _logger = logger;
            _context= context;
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Demo")]
        public IActionResult Get()
        {
            var CurrentMonth = _context.CurrentMonth;

            var CurrentID = _context.CurrentID;
             
            return Ok(new { CurrentMonth, CurrentID });
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "PostTest")]
        [PreventDoublePost]
        public IActionResult PostTest(int Id)
        {  
            return Ok(new { Id });
        }
    }
}
