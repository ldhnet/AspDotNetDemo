using Lee.Repository.Data;
using Lee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;
using System.Linq.Expressions;
using Lee.Repository.Repository;

namespace WebA.Admin.Service
{
    public class EmployeeService: IEmployeeContract
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public async Task<Employee> FindAsync()
        {
            return await _employeeRepository.GetByKeyAsync(1);
        }

        public List<Employee> GetEmployees()
        {
            //var ab = _employeeRepository.GetInclude(c => c.EmployeeDetail);

            //var ac = _employeeRepository.GetInclude(c => c.EmployeeLogins);
             
            //var expr = new Expression<Func<Employee, object>>[] { c => c.EmployeeLogins, c => c.EmployeeDetail };

            //var aa2 = _employeeRepository.Query(expr);

            //var ad = _employeeRepository.GetIncludes(new string[] { "EmployeeLogins", "EmployeeDetail" });


            var aa = _employeeRepository.Entities.ToList();
             
            return _employeeRepository.EntitiesAsNoTracking.ToList();
        }

        public bool SaveEntity(Employee employee)
        {
            int result = _employeeRepository.Insert(employee);
            return result > 0;
        }
        public bool UpdateEntity(Employee employee)
        {
            int result = _employeeRepository.Update(employee);
            return result > 0;
        }
    }
}
