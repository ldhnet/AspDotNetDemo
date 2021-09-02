using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using WebMVC.Context;
using WebMVC.Model;

namespace WebMVC.Service
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class UserRepository : Repository<Employee>,IUserRepository
    { 
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) 
        { 
        }
           
        /// <summary>
        /// 获取账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Employee GetEmployeeInfo(string name)
        { 
            
            return GetAsNoTracking(t=>t.EmployeeName == name);
        }
        public Employee GetEmployeeInfoBySerialNumber(string serialNumber)
        { 
            return GetAsNoTracking(t => t.EmployeeSerialNumber == serialNumber);
        }
        /// <summary>
        /// 更新账号最后一次登入时间
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public void UpdateLoginTime(Employee model)
        {
            Update(model);
        }
    }
}
