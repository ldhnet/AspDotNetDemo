using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Context;
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


    }
}
