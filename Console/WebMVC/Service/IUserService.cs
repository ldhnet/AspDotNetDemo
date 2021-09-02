using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Model;

namespace WebMVC.Service
{
    public interface IUserService
    {
        IUserRepository User { get; }
        Employee Find(string employeeSerialNumber);
    }
}
