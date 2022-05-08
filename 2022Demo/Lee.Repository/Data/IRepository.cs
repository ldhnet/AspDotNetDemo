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
        /// 获取 当前单元操作对象
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        /// <summary>
        /// 获取 当前实体类型的查询数据集AsNoTracking
        /// </summary>
        IQueryable<TEntity> EntitiesAsNoTracking { get; }

        #endregion
        bool Insert(TEntity t);

        Task<bool> InsertAsync(TEntity t);

        bool Update(TEntity t);

        bool Delete(TEntity t); 

        Task<TEntity> GetByIdAsync(TKey id);
        /// <summary>
        /// 获取<typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includePropertySelectors);
        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path);
        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        IQueryable<TEntity> GetIncludes(params string[] paths);
    }
}
