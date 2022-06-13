using Lee.Repository.Data;
using Lee.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Admin.Contracts;
using System.Linq.Expressions;
using Lee.Repository.Repository;

namespace WebA.Admin.Service
{
    public class VisitRecordService : IVisitRecordContract
    {
        private IVisitRecordRepository _visitRecordRepository;

        public VisitRecordService(IVisitRecordRepository visitRecordRepository)
        {
            this._visitRecordRepository = visitRecordRepository;
        }
        public async Task<VisitRecord> FindAsync()
        {
            return await _visitRecordRepository.GetByKeyAsync(1);
        }
         
        public bool SaveEntity(VisitRecord model)
        {
            int result = _visitRecordRepository.Insert(model);
            return result > 0;
        } 
    }
}
