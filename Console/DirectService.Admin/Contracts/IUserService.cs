using DH.Models.Dtos;
using DH.Models.Entities;
using DH.Models.param;
using Framework.Core.Dependency;
using Framework.Utility;

namespace DirectService.Admin.Contracts
{
    public interface IUserService : IDependency
    {
        Task<List<Employee>> GetPageList(Pagination pagination);
        Task<List<Employee>> GetPageList(EmployeeParam param, Pagination pagination);
        BaseResponse CreateInfo(Employee model);
        bool CheckExistsById(string name);
        bool CheckExists(string name);
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
