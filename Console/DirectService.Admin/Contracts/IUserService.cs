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

        IQueryable<Employee> GetAll();
    }
}
