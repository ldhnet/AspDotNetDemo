using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Service
{
    public interface IUserService
    {
        IUserRepository User { get; }
    }
}
