using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Context;
using WebMVC.Controllers;
using WebMVC.Model;

namespace WebMVC.Service
{
    public class UserService : IUserService
    { 
        private IUserRepository _blogRepository;
        private ApplicationDbContext _dataContext = new ApplicationDbContext();
         
        public IUserRepository User
        {
            get
            {
                if (_blogRepository is null)
                {
                    _blogRepository = new UserRepository(_dataContext);
                }
                return _blogRepository;
            }
        }
        public Employee Find(string employeeSerialNumber)
        {
            employeeSerialNumber = employeeSerialNumber.Trim();
            return User.GetEmployeeInfoBySerialNumber(employeeSerialNumber); 
        }

        public Employee FindEmployee(string employeeSerialNumber)
        {
            employeeSerialNumber = employeeSerialNumber.Trim();
            var employee = new Employee();
            using (var _dataContext = new ApplicationDbContext())
            {
                employee = _dataContext.Employees.FirstOrDefault(t => t.EmployeeSerialNumber == employeeSerialNumber);
            }

            return employee;
        }

    }
}
