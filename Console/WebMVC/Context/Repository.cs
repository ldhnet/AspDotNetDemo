using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebMVC.Context
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext dbContext;
        private DbSet<T> dbSet;
        public Repository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<T>();
        }
         
        public int Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
          
        public T GetAsNoTracking(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).AsNoTracking().FirstOrDefault();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).AsNoTracking();
        }

    }
}
