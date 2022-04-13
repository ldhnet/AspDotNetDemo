using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Lee.Repository.Data
{
    /// <summary>
    /// 实体仓储模型的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TEntity, TKey>  where TEntity : class
    {
        #region 属性
         
        /// <summary>
        /// 获取 当前实体类型的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        /// <summary>
        /// 获取 当前实体类型的查询数据集AsNoTracking
        /// </summary>
        IQueryable<TEntity> EntitiesAsNoTracking { get; }

        #endregion
        Task<bool> Insert(TEntity t);
        Task<bool> Update(TEntity t);
        Task<bool> Delete(TEntity t); 
        Task<TEntity> GetByIdAsync(TKey id); 
 
    }
}
