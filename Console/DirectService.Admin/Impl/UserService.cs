using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Core;
using Framework.Core.Data;
using Framework.Utility;

namespace DirectService.Admin.Impl
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
        

        public IQueryable<Employee> GetAll()
        {
            return _userService.EntitiesAsNoTracking;
        }


    }
}
