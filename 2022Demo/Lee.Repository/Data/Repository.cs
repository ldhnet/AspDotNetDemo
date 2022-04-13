using Lee.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata; 
using System.Data;
using System.Linq.Expressions;

namespace Lee.Repository.Data
{
    /// <summary>
    /// EntityFramework的仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;  
        private readonly MyDBContext _dbContext;
        public Repository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        #region 属性

        /// <summary>
        /// 获取 当前实体类型的查询数据集
        /// </summary>
        public IQueryable<TEntity> Entities { get { return _dbSet; } }

        /// <summary>
        /// 获取 当前实体类型的查询数据集AsNoTracking
        /// </summary>
        public IQueryable<TEntity> EntitiesAsNoTracking { get { return _dbSet.AsNoTracking(); } }

        #endregion
        public bool Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }


        public bool Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChanges() > 0;
        }
     

        public bool Delete(TEntity entity)
        { 
            _dbSet.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }
 

        public async Task<TEntity> GetByIdAsync(TKey id)
        { 
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includePropertySelectors)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (includePropertySelectors == null || includePropertySelectors.Length == 0)
            {
                return query;
            }

            foreach (Expression<Func<TEntity, object>> selector in includePropertySelectors)
            {
                query = query.Include(selector);
            }
            return query;
        }

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path)
        { 
            return _dbSet.Include(path);
        }

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetIncludes(params string[] paths)
        { 
            IQueryable<TEntity> source = _dbSet;
            foreach (string path in paths)
            {
                source = source.Include(path);
            }
            return source;
        }

        #region 私有方法
        private bool IsEntityValid(TEntity entity)
        {
            //判断entity是否是_dbContext的Model
            IEntityType entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
            {
                return false;
            }

            //获取主键值名称
            string keyName = entityType.FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();
            if (string.IsNullOrEmpty(keyName))
            {
                return false;
            }

            //获取主键类型
            Type keyType = entityType.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();
            if (keyType == null)
            {
                return false;
            }

            //获取主键值类型的默认值
            object keyDefaultValue = keyType.IsValueType ? Activator.CreateInstance(keyType) : null;

            //获取当前主键值
            object keyValue = entity.GetType().GetProperty(keyName).GetValue(entity, null);

            if (keyDefaultValue.Equals(keyValue))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsEntityTracked(TEntity entity)
        {
            EntityEntry<TEntity> trackedEntity = _dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(o => o.Entity == entity);
            if (trackedEntity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 赋值CreateBy和CreateTime
        /// </summary>
        /// <param name="entity"></param>
        private void AssignCreateProperty(TEntity entity)
        {
            var createByProp = typeof(TEntity).GetProperty("CreateBy");
            var createTimeProp = typeof(TEntity).GetProperty("CreateTime"); 
            createTimeProp?.SetValue(entity, DateTime.Now);
        }
        /// <summary>
        /// 赋值ModifyBy和ModifyTime
        /// </summary>
        /// <param name="entity"></param>
        private void AssignModifyProperty(TEntity entity)
        {
            var modifyByProp = typeof(TEntity).GetProperty("ModifyBy");
            var modifyTimeProp = typeof(TEntity).GetProperty("ModifyTime"); 
            modifyTimeProp?.SetValue(entity, DateTime.Now);
        }
        #endregion
    }
}
