using Framework.Core.Extensions;
using Framework.Utility;
using Framework.Utility.Extensions;
using Framework.Utility.Mapping;
using Framework.Utility.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;

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
            Check.NotNull(entity, nameof(entity));
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
            Check.NotNull(dtos, nameof(dtos));
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
                        ? $"{names.ExpandAndToString()} created successfully"
                        : $"{dtos.Count} items created successfully")
                : new BaseResponse(successCode.NoChanged);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(TEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
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
            Check.NotNull(predicate, nameof(predicate));
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
        public BaseResponse Delete(ICollection<TKey> ids, Action<TEntity>? checkAction = null, Func<TEntity, TEntity>? deleteFunc = null)
        {
            Check.NotNull(ids, nameof(ids));
            List<string> names = new List<string>();
            foreach (TKey id in ids)
            {
                TEntity? entity = _dbSet.Find(id);
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
                        ? $"{names.ExpandAndToString()} deleted successfully "
                        : $"{ids.Count} items deleted successfully")
                : new BaseResponse(successCode.NoChanged);
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Update(TEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
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
            Action<TEditDto>? checkAction = null,
            Func<TEditDto, TEntity, TEntity>? updateFunc = null)
            where TEditDto : IEditDto<TKey>
        {
            Check.NotNull(dtos, nameof(dtos));
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
            return count > 0 ? new BaseResponse(successCode.Success, names.Count > 0 ? $"{names.ExpandAndToString()} edited successfully" : $"{dtos.Count} items edited successfully ")
                : new BaseResponse(successCode.NoChanged);
        }

        /// <summary>
        /// 以DTO为载体更新实体
        /// </summary>
        /// <returns></returns>
        public int UpdateEntity<TEditDto>(TEntity entity, TEditDto dto) where TEditDto : IEditDto<TKey>
        {
            ((DbContext)_unitOfWork).UpdateEntity<TEntity, TEditDto, TKey>(entity, dto);
            return SaveChanges();
        }
        #region Query

        ///<summary>
        ///检查实体是否存在
        ///</summary>
        ///<param name="predicate">查询条件谓语表达式</param>
        ///<param name="id">编辑的实体标识</param>
        ///<param name="keyName">实体的主键名称</param>
        ///<returns>是否存在</returns>
        public bool CheckExists(Expression<Func<TEntity, bool>> predicate, TKey? id = default(TKey), string keyName = "Id")
        {
            Check.NotNull(predicate, nameof(predicate));
            TKey? defaultId = default(TKey);
            var entity = _dbSet.Where(predicate).FirstOrDefault();//SingleOrDefault()
            bool exists = (!(typeof(TKey).IsValueType) && id.Equals(null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !ReflectionHelper.GetObjectPropertyValue<TEntity>(entity, keyName).Equals(id.ToString());
            return exists;
        }
        ///<summary>
        ///检查实体是否存在
        ///</summary>
        ///<param name="predicate">查询条件谓语表达式</param> 
        ///<returns>是否存在</returns>
        public bool CheckExists(Expression<Func<TEntity, bool>> predicate)
        {
            Check.NotNull(predicate, nameof(predicate)); 
            var count = _dbSet.Where(predicate).Count();
            return count>0;
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
        /// 查找第一个符合条件的数据
        /// </summary>
        /// <param name="predicate">数据查询谓语表达式</param>
        /// <returns>符合条件的实体，不存在时返回null</returns>
        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            Check.NotNull(predicate, nameof(predicate));
            return Query(predicate).FirstOrDefault();
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <param name="predicate">数据查询谓语表达式</param>
        /// <returns>符合条件的数据集</returns>
        public IQueryable<TEntity> QueryAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable().AsNoTracking();
            if (predicate == null)
            {
                return query;
            }
            return query.Where(predicate).AsNoTracking();
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源，并可Include导航属性
        /// </summary>
        /// <param name="includePropertySelectors">要Include操作的属性表达式</param>
        /// <returns>符合条件的数据集</returns>
        public IQueryable<TEntity> QueryAsNoTracking(params Expression<Func<TEntity, object>>[] includePropertySelectors)
        {
            return Query(includePropertySelectors).AsNoTracking();
        }

        /// <summary>
        /// 获取<typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        /// <param name="predicate">数据过滤表达式</param>
        /// <returns>符合条件的数据集</returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();
            if (predicate == null)
            {
                return query;
            }
            return query.Where(predicate);
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
            Check.NotNull(path, nameof(path));
            return _dbSet.Include(path);
        }

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        public IQueryable<TEntity> GetIncludes(params string[] paths)
        {
            Check.NotNull(paths, nameof(paths));
            IQueryable<TEntity> source = _dbSet;
            foreach (string path in paths)
            {
                source = source.Include(path);
            }
            return source;
        }

        public async Task<IEnumerable<TEntity>> FindList<TEntity>(Pagination pagination) where TEntity : class, new()
        { 
            var data = await FindPageList<TEntity>(pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            pagination.TotalCount = data.total;
            return data.list;
        }

        public async Task<IEnumerable<TEntity>> FindList<TEntity>(Expression<Func<TEntity, bool>> condition, Pagination pagination) where TEntity : class, new()
        {
            var data = await FindPageList<TEntity>(condition, pagination.Sort, pagination.SortType.ToLower() == "asc" ? true : false, pagination.PageSize, pagination.PageIndex);
            pagination.TotalCount = data.total;
            return data.list;
        }
         
        #endregion Query 

        #region Async

        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            Check.NotNull(entity, nameof(entity));
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
            Check.NotNull(entity, nameof(entity));
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
            Check.NotNull(predicate, nameof(predicate));
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
            Check.NotNull(entity, nameof(entity));
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
            Check.NotNull(predicate, nameof(predicate));
            TKey defaultId = default(TKey);
            var entity = await _dbSet.Where(predicate).SingleOrDefaultAsync();
            bool exists = (!(typeof(TKey).IsValueType) && id.Equals(null)) || id.Equals(defaultId)
                ? entity != null
                : entity != null && !ReflectionHelper.GetObjectPropertyValue<TEntity>(entity, keyName).Equals(id.ToString()); ;
            return exists;
        }

        public async Task<bool> CheckExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Check.NotNull(predicate, nameof(predicate));
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
         
        #endregion Async 

        #region 私有方法

        private int SaveChanges()
        {
            return _unitOfWork.SaveChanges();
        }
        private async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        private async Task<(int total, IEnumerable<T> list)> FindPageList<T>(Expression<Func<T, bool>> condition, string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            var tempData = ((IQueryable<T>)_dbSet).Where(condition);
            return await TupleFindPageList<T>(tempData, sort, isAsc, pageSize, pageIndex);
        }

        private async Task<(int total, IEnumerable<T> list)> FindPageList<T>(string sort, bool isAsc, int pageSize, int pageIndex) where T : class, new()
        {
            var tempData = (IQueryable<T>)_dbSet;
            return await TupleFindPageList(tempData, sort, isAsc, pageSize, pageIndex);
        }

        private async Task<(int total, IEnumerable<T> list)> TupleFindPageList<T>(IQueryable<T>? tempData, string sort, bool isAsc, int pageSize, int pageIndex)
        {
            var total = 0;
            if (tempData == null) return (total, new List<T>()); 
            tempData = DbContextExtensions.AppendSort<T>(tempData, sort, isAsc);
            total = tempData.Count();
            if (total > 0)
            {
                tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
                var list = await tempData.ToListAsync();
                return (total, list);
            }
            else
            {
                return (total, new List<T>());
            }
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
            //var createBy = entity.GetType().GetProperties().FirstOrDefault(c => c.Name == createByProp?.Name)?.GetValue(entity)?.ToString();
            //createByProp?.SetValue(entity, string.IsNullOrEmpty(createBy) ? _principal?.Identity?.Name : createBy);
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
            //var modifyBy = entity.GetType().GetProperties().FirstOrDefault(c => c.Name == modifyByProp?.Name)?.GetValue(entity)?.ToString();
            //modifyByProp?.SetValue(entity, string.IsNullOrEmpty(modifyBy) ? _principal?.Identity?.Name : modifyBy);
            modifyTimeProp?.SetValue(entity, DateTime.Now);//DateTime.UtcNow.ToJsTimestamp()
        }
        #endregion
    }
}
