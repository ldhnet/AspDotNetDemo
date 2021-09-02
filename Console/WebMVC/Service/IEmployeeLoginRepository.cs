using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Threading.Tasks;
using WebMVC.Context;
using WebMVC.Model;
namespace WebMVC.Service
{
    public interface IEmployeeLoginRepository : IRepository<EmployeeLogin>
    {
      
        EmployeeLogin GetEmployeeLoginInfo(int Id);

     
        List<EmployeeLogin> GetEmployeeLoginList();
    }
}
