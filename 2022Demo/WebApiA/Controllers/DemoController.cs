using Microsoft.AspNetCore.Mvc; 
using WebA.Admin.Contracts;
using WebA.Constant;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private readonly ServiceContext _context;
        private readonly MyAdminContext _myContext;
        private readonly IEmployeeContract _employeeContract;

        public DemoController(ILogger<DemoController> logger, 
            ServiceContext context,
            MyAdminContext myContext,
            IEmployeeContract employeeContract)
        {
            _logger = logger;
            _context= context;
            _myContext= myContext;
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


            var CurrentMonth1 = _myContext.CurrentMonth;

            var CurrentID1 = _myContext.CurrentID;

             
            var list =  _employeeContract.GetEmployees();


            return Ok(new { CurrentMonth1, CurrentID1, CurrentMonth, CurrentID, list });
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
