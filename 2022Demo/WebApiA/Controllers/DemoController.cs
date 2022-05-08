using Microsoft.AspNetCore.Mvc;
using WebA.Admin;
using WebA.Admin.Contracts;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private ServiceContext _context;

        private IEmployeeContract _employeeContract;

        public DemoController(ILogger<DemoController> logger, ServiceContext context, IEmployeeContract employeeContract)
        {
            _logger = logger;
            _context= context;
            _employeeContract = employeeContract;
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

            var list =  _employeeContract.GetEmployees();


            return Ok(new { CurrentMonth, CurrentID, list });
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
