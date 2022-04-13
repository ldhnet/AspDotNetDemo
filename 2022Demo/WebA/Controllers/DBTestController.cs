using Lee.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using WebA.Admin.Contracts;

namespace WebA.Controllers
{
    public class DBTestController : Controller
    {
        private readonly ILogger<DBTestController> _logger;
        private readonly IEmployeeContract _employeeContract;
        public DBTestController(ILogger<DBTestController> logger, IEmployeeContract employeeContract)
        {
            _logger = logger;
            _employeeContract = employeeContract;
        }
        public IActionResult Index()
        {

            return View();
        }
        public List<Employee> GetEmployeeList()
        {
            _logger.LogInformation("GetEmployeeList");
            return _employeeContract.GetEmployees();
        }
        public void SaveEmployee()
        {
            Random rd = new Random();
            Employee emp = new Employee();
            emp.Name = "张三" + rd.Next(99);
            emp.EmployeeName = "张三" + rd.Next(99);
            emp.EmployeeSerialNumber = rd.Next(1000, 9999).ToString();
            emp.Department = rd.Next(5);
            emp.BankCard = "1000000" + rd.Next(1000, 9999);
            emp.Phone = "1522507" + rd.Next(10000, 99999);
            emp.ExpirationDateUtc = DateTime.Now.AddYears(1);
            emp.CreateTime = DateTime.Now;

            emp.EmployeeDetail = new EmployeeDetail()
            {
                EnglishName = "zhangsan" + rd.Next(99),
                CreateTime = DateTime.Now,
            };

            emp.EmployeeLogins.Add(new EmployeeLogin
            {

                CreateTime = DateTime.Now
            });

            var aaa2255 = _employeeContract.SaveEntity(emp); 
        }
    }
}
