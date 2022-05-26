using Lee.EF.Context;
using Lee.Models.Entities;
using Lee.Repository.Data;
using Lee.Repository.Repository;
using NUnit.Framework;
using WebA.Admin.Contracts;
using WebA.Admin.Service;

namespace NUnitTest
{
    [TestFixture]
    public class Tests
    {
        private IEmployeeContract _employeeContract;
        private IEmployeeRepository _employeeRepository;
        [SetUp]
        public void Setup()
        {
            MyDBContext _context=new MyDBContext();
            _employeeContract = new EmployeeService(_employeeRepository);
        }

        [Test]
        public void Test1()
        {
           var aaa=  _employeeContract.GetEmployees();
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var aaa = _employeeContract.FindAsync();
            Assert.Pass();
        }
    }
}