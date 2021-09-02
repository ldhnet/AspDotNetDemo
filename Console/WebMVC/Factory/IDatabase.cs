using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Context
{
    public interface IDatabase
    {
        #region 属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public DbContext dbContext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public IDbContextTransaction dbContextTransaction { get; set; }
        #endregion

        IDatabase BeginTrans();
        int CommitTrans();
        void RollbackTrans();
        void Close();

        Task<IDatabase> BeginTransAsync();
        Task<int> CommitTransAsync();
        Task RollbackTransAsync();
        Task CloseAsync();


        T FindEntity<T>(object KeyValue) where T : class;
    }
}
