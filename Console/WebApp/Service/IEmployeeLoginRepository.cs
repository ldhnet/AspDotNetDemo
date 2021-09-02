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
    public interface IEmployeeLoginRepository : IRepository<EmployeeLogin>
    {
      
        EmployeeLogin GetEmployeeLoginInfo(int Id);

     
        List<EmployeeLogin> GetEmployeeLoginList();
    }
}
