using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Extensions;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using WebMVC.Extension;
using Framework.Utility.Extensions;
using Framework.Utility.Reflection;
using Framework.Utility;

namespace Framework.Core.Data
{
    /// <summary>
    /// EntityFramework的仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class// EntityBase<TKey>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClaimsPrincipal _principal;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = ((DbContext)unitOfWork).Set<TEntity>();
        }
        /// <summary>
        /// 初始化一个<see cref="Repository{TEntity, TKey}"/>类型的新实例
        /// </summary>
        public Repository(IUnitOfWork unitOfWork, IPrincipal principal)
        {
            _unitOfWork = unitOfWork;
            _dbSet = ((DbContext)unitOfWork).Set<TEntity>(); 
            _principal = principal as ClaimsPrincipal; 
        }

        /// <summary>
        /// 获取 当前单元操作对象
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        /// <summary>
        /// 获取 当前实体类型的查询数据集
        /// </summary>
        public IQueryable<TEntity> Entities { get { return _dbSet; } }

        /// <summary>
        /// 获取 当前实体类型的查询数据集AsNoTracking
        /// </summary>
        public IQueryable<TEntity> EntitiesAsNoTracking { get { return _dbSet.AsNoTracking(); } }
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Insert(TEntity entity)
        {
            entity.CheckNotNull("entity");
            AssignCreateProperty(entity);
            _dbSet.Add(entity);
            return SaveChanges();
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Insert(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            foreach (var item in entities)
            {
                AssignCreateProperty(item);
            }
            _dbSet.AddRange(entities);
            return SaveChanges();
        }

        /// <summary>
        /// 以DTO为载体批量插入实体
        /// </summary>
        /// <typeparam name="TAddDto">添加DTO类型</typeparam>
        /// <param name="dtos">添加DTO信息集合</param>
        /// <param name="checkAction">添加信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        public BaseResponse Insert<TAddDto>(ICollection<TAddDto> dtos,
            Action<TAddDto>? checkAction = null,
            Func<TAddDto, TEntity, TEntity>? updateFunc = null)
            where TAddDto : IAddDto
        {
            dtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            foreach (TAddDto dto in dtos)
            {
                try
                {
                    if (checkAction != null)
                    {
                        checkAction(dto);
                    }
                    TEntity entity = dto.MapTo<TEntity>();
                    if (updateFunc != null)
                    {
                        entity = updateFunc(dto, entity);
                    }
                    AssignCreateProperty(entity);
                    _dbSet.Add(entity);
                }
                catch (Exception e)
                {
                    return new BaseResponse(successCode.Error, e.Message);
                }
                string name = GetNameValue(dto);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = SaveChanges();
            return count > 0
                ? new BaseResponse(successCode.Success,
                    names.Count > 0
                        ? $"{0} created successfully {names.ExpandAndToString()}"
                        : $"{0} items created successfully {dtos.Count}")
                : new BaseResponse(successCode.NoChanged);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(TEntity entity)
        {
            entity.CheckNotNull("entity");
            _dbSet.Remove(entity);
            return SaveChanges();
        }

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public virtual int Delete(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = _dbSet.Find(key);
            return entity == null ? 0 : Delete(entity);
        }

        /// <summary>
        /// 删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = _dbSet.Where(predicate).ToArray();
            return entities.Length == 0 ? 0 : Delete(entities);
        }

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            _dbSet.RemoveRange(entities);
            return SaveChanges();
        }

        /// <summary>
        /// 以标识集合批量删除实体
        /// </summary>
        /// <param name="ids">标识集合</param>
        /// <param name="checkAction">删除前置检查委托</param>
        /// <param name="deleteFunc">删除委托，用于删除关联信息</param>
        /// <returns>业务操作结果</returns>
        public BaseResponse Delete(ICollection<TKey> ids, Action<TEntity> checkAction = null, Func<TEntity, TEntity> deleteFunc = null)
        {
            ids.CheckNotNull("ids");
            List<string> names = new List<string>();
            foreach (TKey id in ids)
            {
                TEntity entity = _dbSet.Find(id);
                try
                {
                    if (checkAction != null)
                    {
                        checkAction(entity);
                    }
                    if (deleteFunc != null)
                    {
                        entity = deleteFunc(entity);
                    }
                    _dbSet.Remove(entity);
                }
                catch (Exception e)
                {
                    return new BaseResponse(successCode.Error, e.Message);
                }
                string name = GetNameValue(entity);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = SaveChanges();
            return count > 0
                ? new BaseResponse(successCode.Success,
                    names.Count > 0
                        ? $"{0} deleted successfully {names.ExpandAndToString()}"
                        : $"{0} items deleted successfully {ids.Count}" )
                : new BaseResponse(successCode.NoChanged);
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Update(TEntity entity)
        {
            entity.CheckNotNull("entity");
            AssignModifyProperty(entity);
            ((DbContext)_unitOfWork).Update<TEntity, TKey>(entity);
            return SaveChanges();
        }

        /// <summary>
        /// 以DTO为载体批量更新实体
        /// </summary>
        /// <typeparam name="TEditDto">更新DTO类型</typeparam>
        /// <param name="dtos">更新DTO信息集合</param>
        /// <param name="checkAction">更新信息合法性检查委托</param>
        /// <param name="updateFunc">由DTO到实体的转换委托</param>
        /// <returns>业务操作结果</returns>
        public BaseResponse Update<TEditDto>(ICollection<TEditDto> dtos,
            Action<TEditDto> checkAction = null,
            Func<TEditDto, TEntity, TEntity> updateFunc = null)
            where TEditDto : IEditDto<TKey>
        {
            dtos.CheckNotNull("dtos");
            List<string> names = new List<string>();
            foreach (TEditDto dto in dtos)
            {
                try
                {
                    if (checkAction != null)
                    {
                        checkAction(dto);
                    }
                    TEntity entity = _dbSet.Find(dto.Id);
                    if (entity == null)
                    {
                        return new BaseResponse(successCode.QueryNull);
                    }
                    entity = dto.MapTo(entity);
                    if (updateFunc != null)
                    {
                        entity = updateFunc(dto, entity);
                    }
                    AssignModifyProperty(entity);
                    ((DbContext)_unitOfWork).Update<TEntity, TKey>(entity);
                }
                catch (Exception e)
                {
                    return new BaseResponse(successCode.Error, e.Message);
                }
                string name = GetNameValue(dto);
                if (name != null)
                {
                    names.Add(name);
                }
            }
            int count = SaveChanges();
            return count > 0 ? new BaseResponse(successCode.Success,  names.Count > 0 ? $"{0} edited successfully {names.ExpandAndToString()}"  : $"{0} items edited successfully {dtos.Count}")
                : new BaseResponse(successCode.NoChanged);
        }

        ///<summary>
        ///检查实体是否存在
        ///</summary>
        ///<param name="predicate">查询条件谓语表达式</param>
        ///<param name="id">编辑的实体标识</param>
        ///<param name="keyName">实体的主键名称</param>
        ///<returns>是否存在</returns>
        public bool CheckExists(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey), string keyName = "Id")
        {
            predicate.CheckNotNull("predicate");
            TKey defaultId = default(TKey);
            var entity = _dbSet.Where(predicate).SingleOrDefault();
            bool exists = (!(typeof(TKey).IsValueType) && id.Equals(null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !ReflectionHelper.GetObjectPropertyValue<TEntity>(entity, keyName).Equals(id.ToString());
            return exists;
        }

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public TEntity GetByKey(TKey key)
        {
            CheckEntityKey(key, "key");
            return _dbSet.Find(key);
        }

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetInclude<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            path.CheckNotNull("path");
            return _dbSet.Include(path);
        }

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetIncludes(params string[] paths)
        {
            paths.CheckNotNull("paths");
            IQueryable<TEntity> source = _dbSet;
            foreach (string path in paths)
            {
                source = source.Include(path);
            }
            return source;
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 
        /// 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 
        /// 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        { 
            return trackEnabled  ? _dbSet.FromSqlRaw(sql, parameters) : _dbSet.FromSqlRaw(sql, parameters).AsNoTracking();
        }
         
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            AssignCreateProperty(entity);
            _dbSet.Add(entity);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();

            foreach (var item in entities)
            {
                AssignCreateProperty(item);
            }

            _dbSet.AddRange(entities);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 异步删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            TEntity entity = await _dbSet.FindAsync(key);
            return entity == null ? 0 : await DeleteAsync(entity);
        }

        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            TEntity[] entities = await _dbSet.Where(predicate).ToArrayAsync();
            return entities.Length == 0 ? 0 : await DeleteAsync(entities);
        }

        /// <summary>
        /// 异步批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            entities = entities as TEntity[] ?? entities.ToArray();
            _dbSet.RemoveRange(entities);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 异步更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            entity.CheckNotNull("entity");
            AssignModifyProperty(entity);
            ((DbContext)_unitOfWork).Update<TEntity, TKey>(entity);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <param name="keyName">实体的主键名称</param>
        /// <returns>是否存在</returns>
        public async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate, TKey id = default(TKey), string keyName = "Id")
        {
            predicate.CheckNotNull("predicate");
            TKey defaultId = default(TKey);
            var entity = await _dbSet.Where(predicate).SingleOrDefaultAsync();
            bool exists = (!(typeof(TKey).IsValueType) && id.Equals(null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !ReflectionHelper.GetObjectPropertyValue<TEntity>(entity, keyName).Equals(id.ToString()); ;
            return exists;
        }

        public async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            predicate.CheckNotNull("predicate");
            var count = await _dbSet.Where(predicate).CountAsync();
            return count > 0;
        }
        /// <summary>
        /// 异步查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public async Task<TEntity> GetByKeyAsync(TKey key)
        {
            CheckEntityKey(key, "key");
            return await _dbSet.FindAsync(key);
        }


        #region 私有方法

        private int SaveChanges()
        {
            return _unitOfWork.SaveChanges();
        }


        private async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }


        private static void CheckEntityKey(object key, string keyName)
        {
            key.CheckNotNull("key");
            keyName.CheckNotNull("keyName");

            Type type = key.GetType();
            if (type == typeof(int))
            {
                ((int)key).CheckGreaterThan(keyName, 0);
            }
            else if (type == typeof(string))
            {
                ((string)key).CheckNotNullOrEmpty(keyName);
            }
            else if (type == typeof(Guid))
            {
                ((Guid)key).CheckNotEmpty(keyName);
            }
        }

        private static string GetNameValue(object value)
        {
            dynamic obj = value;
            try
            {
                return obj.Name;
            }
            catch
            {
                return null;
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

            createByProp?.SetValue(entity, _principal?.Identity?.Name);
            createTimeProp?.SetValue(entity, DateTime.UtcNow.ToJsTimestamp());
        }
        /// <summary>
        /// 赋值ModifyBy和ModifyTime
        /// </summary>
        /// <param name="entity"></param>
        private void AssignModifyProperty(TEntity entity)
        {
            var modifyByProp = typeof(TEntity).GetProperty("ModifyBy");
            var modifyTimeProp = typeof(TEntity).GetProperty("ModifyTime");

            modifyByProp?.SetValue(entity, _principal?.Identity?.Name);
            modifyTimeProp?.SetValue(entity, DateTime.UtcNow.ToJsTimestamp());
        }
        #endregion
    }
}
