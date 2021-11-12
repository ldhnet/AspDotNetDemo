using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Core.Data;
using Framework.Utility;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace DirectService.Admin.Impl
{
    public class UserService : IUserService
    { 
        private IRepository<Employee, int> _userService; 
        public UserService(IRepository<Employee, int> userService)
        {
            this._userService = userService; 
        }

        public Employee Find(string employeeSerialNumber)
        {
            employeeSerialNumber = employeeSerialNumber.Trim();
            return _userService.EntitiesAsNoTracking.FirstOrDefault(c=>c.EmployeeSerialNumber == employeeSerialNumber)??new Employee(); 
        }

        public BaseResponse<Employee> FindEmployee(string employeeSerialNumber)
        {
            employeeSerialNumber = employeeSerialNumber.Trim(); 
            var employee = _userService.EntitiesAsNoTracking.FirstOrDefault(c=>c.EmployeeSerialNumber == employeeSerialNumber); 
            return new BaseResponse<Employee>(successCode.Success, "", employee??new Employee());
        }

    }
}
