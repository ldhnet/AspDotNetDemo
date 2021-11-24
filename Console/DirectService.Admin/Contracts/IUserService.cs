using DH.Models.Dtos;
using DH.Models.Entities; 
using Framework.Core.Dependency;
using Framework.Utility;

namespace DirectService.Admin.Contracts
{
    public interface IUserService : IDependency
    {
        BaseResponse CreateInfo(Employee model);

        BaseResponse CreateInfo(params EmployeeDto[] dtos);
        Employee Find(string employeeSerialNumber);
        BaseResponse UpdateEmployee(params EmployeeDto[] dtos);
        BaseResponse UpdateEmployee(Employee Employee);
        BaseResponse<Employee> FindEmployee(string employeeSerialNumber);
        Employee? GetUserByToken(string token);

        IQueryable<Employee> GetAllList();
        IQueryable<Employee> GetAll();
    }
}
