using Lee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Admin.Contracts
{
    public interface ITestContract
    {
        Task<Biz_Test> FindAsync(); 
        List<Biz_Test> GetList();

        bool SaveEntity(Biz_Test employee);

        bool UpdateEntity(Biz_Test employee);
    }
}
