using Lee.Models.Entities;
using Lee.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Repository.Repository
{
    public class VisitRecordRepository : Repository<VisitRecord, int>, IVisitRecordRepository
    {
        public VisitRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
