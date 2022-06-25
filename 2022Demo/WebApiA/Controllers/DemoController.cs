using Lee.Models;
using Lee.Models.Entities;
using Lee.Utility.Extensions;
using Lee.Utility.Security;
using Microsoft.AspNetCore.Mvc;
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

        public DemoController(ILogger<DemoController> logger, IEmployeeContract employeeContract)
        {
            _logger = logger; 
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
            var list = _employeeContract.GetEmployees();
            foreach (var item in list)
            {
                var aa = item.EmployeeStatus.ToDescription();
                Console.WriteLine(aa);
                _logger.LogInformation(aa);
            }
             
            return Ok(new { list });
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
            emp.EmployeeSerialNumber = "56789";
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
