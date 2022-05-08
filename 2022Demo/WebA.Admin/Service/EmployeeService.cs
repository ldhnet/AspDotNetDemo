using Lee.Repository.Data;
using Lee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;
using System.Linq.Expressions;

namespace WebA.Admin.Service
{
    public class EmployeeService: IEmployeeContract
    {
        private IRepository<Employee, int> _employeeRepository;

        public EmployeeService(IRepository<Employee, int> employeeService)
        {
            this._employeeRepository = employeeService;
        }
        public async Task<Employee> FindAsync()
        {
            return await _employeeRepository.GetByIdAsync(1);
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
            return _employeeRepository.Insert(employee);
        }
        public bool UpdateEntity(Employee employee)
        {
            return _employeeRepository.Update(employee);
        }
    }
}
