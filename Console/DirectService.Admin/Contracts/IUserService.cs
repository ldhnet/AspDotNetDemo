using DH.Models.DbModels;
using Framework.Core.Dependency;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace DirectService.Admin.Contracts
{
    public interface IUserService: IDependency
    {
        BaseResponse CreateInfo(Employee model);
        Employee Find(string employeeSerialNumber);
        BaseResponse<Employee> FindEmployee(string employeeSerialNumber);

        IQueryable<Employee> GetAll();
    }
}
