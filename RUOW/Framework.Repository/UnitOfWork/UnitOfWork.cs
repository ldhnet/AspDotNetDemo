using Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;

namespace Framework.Repository
{ 
    public  class UnitOfWork : DefaultDbContext, IUnitOfWork
    { 
        /// <summary>
        /// 获取 是否开启事务提交
        /// </summary>
        public bool TransactionEnabled => Database.CurrentTransaction != null;

        /// <summary>
        /// 显式开启数据上下文事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (Database.CurrentTransaction == null)
            {
                Database.BeginTransaction(isolationLevel);
            }
        }
        /// <summary>
        /// 提交事务的更改
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            IDbContextTransaction transaction = Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return 0;
        }


        /// <summary>
        /// 显式回滚事务，仅在显式开启事务后有用
        /// </summary>
        public void Rollback()
        {
            if (Database.CurrentTransaction != null)
            {
                Database.CurrentTransaction.Rollback();
            }
        }

        /// <summary>
        /// 提交当前单元操作的更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                int count = base.SaveChanges();
                return count;
            }
            catch (DbUpdateException e)
            {
                Rollback();
                throw;
            }
        }


        # region 异步

        /// <summary>
        /// 异步显式开启数据上下文事务
        /// </summary>
        /// <param name="isolationLevel"></param>
        public async  Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Database.CurrentTransaction == null)
            {
               await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            }
        }

        /// <summary>
        /// 异步提交事务的更改
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            IDbContextTransaction transaction = Database.CurrentTransaction;
            if (transaction != null)
            {
                try
                {
                    await transaction.CommitAsync();
                    return 1;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return 0;
        }

        /// <summary>
        /// 异步显式回滚事务，仅在显式开启事务后有用
        /// </summary>
        public async Task RollbackAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Database.CurrentTransaction != null)
            {
               await Database.CurrentTransaction.RollbackAsync(cancellationToken);
            }
        }
        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns></returns>
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await base.SaveChangesAsync(); 
            }
            catch (DbUpdateException e)
            {
                await RollbackAsync();
                throw e;
            }
        }

        #endregion

        #region sql
        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlRaw(sql, parameters);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters) where TElement : new()
        {
            var conn = Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    var properties = typeof(TElement).GetTypeInfo().DeclaredProperties;

                    var result = new List<TElement>();
                    TElement model;
                    object val;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new TElement();
                            foreach (var prop in properties)
                            {
                                val = reader[prop.Name];
                                if (val == DBNull.Value)
                                    prop.SetValue(model, null);
                                else
                                    prop.SetValue(model, val);

                            }
                            result.Add(model);
                        }
                    }

                    return result;
                }
            }
            finally
            {
                conn.Close();
            }
        }
         
        #endregion
    }
}
