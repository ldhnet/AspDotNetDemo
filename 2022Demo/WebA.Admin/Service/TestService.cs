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
    public class TestService : ITestContract
    {
        private ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            this._testRepository = testRepository;
        }
        public async Task<Biz_Test> FindAsync()
        {
            return await _testRepository.GetByKeyAsync(1);
        }

        public List<Biz_Test> GetList()
        {
            //var ab = _employeeRepository.GetInclude(c => c.EmployeeDetail);

            //var ac = _employeeRepository.GetInclude(c => c.EmployeeLogins);
             
            //var expr = new Expression<Func<Employee, object>>[] { c => c.EmployeeLogins, c => c.EmployeeDetail };

            //var aa2 = _employeeRepository.Query(expr);

            //var ad = _employeeRepository.GetIncludes(new string[] { "EmployeeLogins", "EmployeeDetail" });


            var aa = _testRepository.Entities.ToList();
             
            return _testRepository.EntitiesAsNoTracking.ToList();
        }

        public bool SaveEntity(Biz_Test employee)
        {
            int result = _testRepository.Insert(employee);
            return result > 0;
        }
        public bool UpdateEntity(Biz_Test employee)
        {
            int result = _testRepository.Update(employee);
            return result > 0;
        }
    }
}
