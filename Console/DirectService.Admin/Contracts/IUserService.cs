using DH.Models.DbModels;
using Framework.Core.Dependency;
using Framework.Utility;

namespace DirectService.Admin.Contracts
{
    public interface IUserService : IDependency
    {
        BaseResponse CreateInfo(Employee model);
        Employee Find(string employeeSerialNumber);
        BaseResponse<Employee> FindEmployee(string employeeSerialNumber);
        Employee? GetUserByToken(string token);

        IQueryable<Employee> GetAllList();
        IQueryable<Employee> GetAll();
    }
}
