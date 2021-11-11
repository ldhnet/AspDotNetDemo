using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DH.Models.DbModels
{
    public partial class MyDBContext : IUnitOfWork
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

        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                int count = await base.SaveChangesAsync();
                return count;
            }
            catch (DbUpdateException e)
            {
                Rollback();
                throw;
            }
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
    }
}
