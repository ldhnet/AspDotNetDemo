using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata; 
using System.Data; 

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
        private readonly DbContext dbContext; 
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

        public async Task<bool> Insert(TEntity entity)
        { 
            _dbSet.Add(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(TEntity entity)
        {  
            _dbSet.Update(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(TEntity entity)
        { 
            _dbSet.Remove(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }
 

        public async Task<TEntity> GetByIdAsync(TKey id)
        { 
            return await _dbSet.FindAsync(id);
        }
         
        #region 私有方法
        private bool IsEntityValid(TEntity entity)
        {
            //判断entity是否是DbContext的Model
            IEntityType entityType = dbContext.Model.FindEntityType(typeof(TEntity));
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
            EntityEntry<TEntity> trackedEntity = dbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(o => o.Entity == entity);
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
