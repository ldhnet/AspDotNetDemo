using Lee.Models;
using Lee.Models.Entities;
using Lee.Utility.Extensions;
using Microsoft.AspNetCore.Mvc; 
using WebA.Admin.Contracts;
using WebA.Constant;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]  
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
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var CurrentMonth = _context.CurrentMonth;

            var CurrentID = _context.CurrentID;


            var CurrentMonth1 = _myContext.CurrentMonth;

            var CurrentID1 = _myContext.CurrentID;

             
            var list =  _employeeContract.GetEmployees();

            foreach (var item in list)
            {
                var aa = item.EmployeeStatus.ToDescription();
                Console.WriteLine(aa);
            }


            return Ok(new { CurrentMonth1, CurrentID1, CurrentMonth, CurrentID, list });
        }



        /// <summary>
        /// 测试禁止重复提交demo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PostTest")]
        [PreventDoublePost] 
        public IActionResult PostTest(int Id)
        {  
            return Ok(new { Id });
        }


        /// <summary>
        /// 测试新增人员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee(string name)
        {
            var aaa = EmployeeStatus.PendingStatus.ToDescription();
             
            var ccc = typeof(EmployeeStatus).ToEnumForDictionary();
              
            Employee emp= new  Employee();
            emp.Name = name; 
            emp.EmployeeName = name;
            emp.EmployeeSerialNumber = "12345";
            emp.Department = 1;
            emp.EmployeeStatus = EmployeeStatus.PendingStatus;
            emp.Phone = "15225074031";
            emp.BankCard = "15225074031";
            emp.WebToken = "12345";
            emp.ApiToken = "12345";
            emp.ExpirationDateUtc = DateTime.Now;
            emp.CreateTime = DateTime.Now;
            var result =  _employeeContract.SaveEntity(emp);
            return Ok(result);
        }

    }
}
