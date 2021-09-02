using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApp.Context;
using WebApp.Model;
namespace WebApp.Service
{
    public interface IUserRepository : IRepository<Employee>
    {
        /// <summary>
        /// 获取账号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Employee GetEmployeeInfo(string name);

        /// <summary>
        /// 更新账号最后一次登入时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void UpdateLoginTime(Employee model);
    }
}
