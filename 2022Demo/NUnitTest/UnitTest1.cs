using Lee.EF.Context;
using Lee.Models.Entities;
using Lee.Repository.Data;
using NUnit.Framework;
using WebA.Admin.Contracts;
using WebA.Admin.Service;

namespace NUnitTest
{
    [TestFixture]
    public class Tests
    {
        private IEmployeeContract _employeeContract;
        private IRepository<Employee, int> _employeeRepository;
        [SetUp]
        public void Setup()
        {
            MyDBContext _context=new MyDBContext();
            _employeeRepository = new Repository<Employee, int>(_context);
            _employeeContract = new EmployeeService(_employeeRepository);
        }

        [Test]
        public void Test1()
        {
           var aaa=  _employeeContract.GetEmployees();
            Assert.Pass();
        }
    }
}