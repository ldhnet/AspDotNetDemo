using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Context
{
    public interface IRepository<T> where T : class
    { 
        int Update(T entity);       
        T GetAsNoTracking(Expression<Func<T, bool>> where);

        IQueryable<T> GetAll(Expression<Func<T, bool>> where);

    } 
}
