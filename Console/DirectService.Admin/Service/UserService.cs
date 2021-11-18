using DH.Models.DbModels;
using DirectService.Admin.Contracts; 
using Framework.Core.Data;
using Framework.Utility;
using System.Linq.Expressions;

namespace DirectService.Admin.Service
{
    public class UserService : IUserService
    {
        private IRepository<Employee, int> _userService;
        public UserService(IRepository<Employee, int> userService)
        {
            this._userService = userService;
        }

        public BaseResponse CreateInfo(Employee model)
        {
            var ret = _userService.Insert(model);
            return ret > 0 ? new BaseResponse(successCode.Success) : new BaseResponse(successCode.NoChanged);
        }

        public Employee Find(string employeeSerialNumber)
        { 
            employeeSerialNumber = employeeSerialNumber.Trim();
            return _userService.EntitiesAsNoTracking.FirstOrDefault(c => c.EmployeeSerialNumber == employeeSerialNumber) ?? new Employee();
        }

        public BaseResponse<Employee> FindEmployee(string employeeSerialNumber)
        {
            employeeSerialNumber = employeeSerialNumber.Trim();
            var employee = _userService.EntitiesAsNoTracking.FirstOrDefault(c => c.EmployeeSerialNumber == employeeSerialNumber);
            return new BaseResponse<Employee>(successCode.Success, "", employee ?? new Employee());
        }

        public Employee? GetUserByToken(string token)
        {
            token = token.Trim();
            return _userService.EntitiesAsNoTracking.FirstOrDefault(c => c.ApiToken == token);
        }

        public IQueryable<Employee> GetAllList()
        {
            return _userService.Entities;
        }
        public IQueryable<Employee> GetAll()
        {
            //var aa= _userService.GetInclude(c => c.EmployeeLogins).Where(c => true);

            var aa3 = _userService.GetByKey(1);

            Expression<Func<Employee,object>> expr1 = c => c.EmployeeLogins;

            Expression<Func<Employee, object>> expr2 = c => c.EmployeeDetail;

            var expr = new Expression<Func<Employee, object>>[] { c => c.EmployeeLogins, c => c.EmployeeDetail };


            var aa2 = _userService.QueryAsNoTracking(expr);

            var aa = _userService.GetIncludes(new string[] { "EmployeeLogins", "EmployeeDetail" });
            return aa;
        }
         
    }
}
