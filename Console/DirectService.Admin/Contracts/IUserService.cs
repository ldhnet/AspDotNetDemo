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
        Employee Find(string employeeSerialNumber);
        BaseResponse<Employee> FindEmployee(string employeeSerialNumber);
    }
}
