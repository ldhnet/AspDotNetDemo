using Lee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Admin.Contracts
{
    public interface IEmployeeContract
    {
        Task<Employee> FindAsync(); 
        List<Employee> GetEmployees();
    }
}
