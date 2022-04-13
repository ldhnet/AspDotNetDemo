using Lee.Repository.Data;
using Lee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;

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
            return _employeeRepository.EntitiesAsNoTracking.ToList();
        }
    }
}
