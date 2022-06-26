using Lee.Models.Entities;
using Lee.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Repository.Repository
{
    public class TestRepository : Repository<Biz_Test, int>, ITestRepository
    {
        public TestRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
