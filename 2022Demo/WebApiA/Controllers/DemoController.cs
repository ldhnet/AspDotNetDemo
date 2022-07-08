using Lee.Models;
using Lee.Models.Entities;
using Lee.Utility.Extensions;
using Lee.Utility.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Text;
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
        private readonly IEmployeeContract _employeeContract;
        private readonly ITestContract _testContract;
        public DemoController(ILogger<DemoController> logger, IEmployeeContract employeeContract, ITestContract testContract)
        {
            _logger = logger; 
            _employeeContract = employeeContract;
            _testContract = testContract;
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var list = _employeeContract.GetEmployees();
            foreach (var item in list)
            {
                var aa = item.EmployeeStatus.ToDescription();
                Console.WriteLine(aa);
                _logger.LogInformation(aa);
            }
            var aaw =new ConcurrentDictionary<int, string>();

          

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/flowdata.json");



            return Ok(new {msg="",code=200, data = list });
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
              
            Employee emp= new  Employee();
            emp.Name = name; 
            emp.EmployeeName = name;
            emp.EmployeeSerialNumber = "123456";
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


        /// <summary>
        /// 测试新增test
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddTest")]
        public IActionResult AddTest(string name)
        {
            Biz_Test tt = new Biz_Test();
            tt.Name = name; 
            tt.Phone = "15225074031";

            tt.TestStatus = EmployeeStatus.PendingStatus;

            tt.BirthDate = DateTime.Now;
            tt.LeaveDate = DateTime.Now.Date;
            tt.CreateTime = DateTime.Now;

            var result = _testContract.SaveEntity(tt);

            var list = _testContract.GetList();

            var aaa = tt.TestStatus.HasFlag(EmployeeStatus.WaitStatus);

            return Ok(list);
        }



        private void enumTest()
        {
            var aaa = EmployeeStatus.PendingStatus.ToDescription();
            var ccc = typeof(EmployeeStatus).ToEnumForDictionary();
        }
        private void AesTest()
        {
            var a = "123";
            var aaaa = SecurityHelper.Encrypt(a);
            var bbbb = SecurityHelper.Decrypt(aaaa);

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ldh.docx");
            var filepath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ldh2.docx");
            var filepath3 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ldh3.docx");

            filepath.CheckFileExists("ldhdd");
            SecurityHelper.EncryptFile(filepath, filepath2);
            SecurityHelper.DecryptFile(filepath2, filepath3); 
        }

    }
}
