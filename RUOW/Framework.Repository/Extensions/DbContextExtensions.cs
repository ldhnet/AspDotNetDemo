using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Common; 
using System.Text;

namespace Framework.Core.Extensions
{
    public static class DbContextExtensions
    {

        public static DateTime DefaultTime = DateTime.Parse("1970-01-01 00:00:00");

        /// <summary>
        /// 更新上下文中指定的实体的状态
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="dbContext">上下文对象</param>
        /// <param name="entities">要更新的实体类型</param>
        public static void Update<TEntity, TKey>(this DbContext dbContext, params TEntity[] entities)
            where TEntity : class
        { 
            DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
            foreach (TEntity entity in entities)
            {
                try
                {
                    var entry = dbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    //TEntity oldEntity = dbSet.Find(entity.Id);
                    //dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 以DTO为载体更新实体
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEditDto"></typeparam>
        public static void UpdateEntity<TEntity, TEditDto, TKey>(this DbContext dbContext, TEntity entity, TEditDto dto)
            where TEntity : class
        {  
            dbContext.Entry(entity).CurrentValues.SetValues(dto);
        }

        /// <summary>
        /// 清除数据上下文的更改
        /// </summary>
        public static void CleanChanges(this DbContext context)
        {
            IEnumerable<EntityEntry> entries = context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static string DeleteSql(string tableName)
        {
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + "");
            return strSql.ToString();
        }

        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public static string DeleteSql(string tableName, string propertyName, long propertyValue)
        {
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + propertyName + " = " + propertyValue + "");
            return strSql.ToString();
        }

        /// <summary>
        /// 拼接批量删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public static string DeleteSql(string tableName, string propertyName, long[] propertyValue)
        {
            string strSql = "DELETE FROM " + tableName + " WHERE " + propertyName + " IN (" + string.Join(",", propertyValue) + ")";
            return strSql.ToString();
        }

        /// <summary>
        /// 获取实体映射对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbcontext"></param>
        /// <returns></returns>
        public static IEntityType GetEntityType<T>(DbContext dbcontext) where T : class
        {
            return dbcontext.Model.FindEntityType(typeof(T));
        }

        /// <summary>
        /// 存储过程语句
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static string BuilderProc(string procName, params DbParameter[] dbParameter)
        {
            StringBuilder strSql = new StringBuilder("exec " + procName);
            if (dbParameter != null)
            {
                foreach (var item in dbParameter)
                {
                    strSql.Append(" " + item + ",");
                }
                strSql = strSql.Remove(strSql.Length - 1, 1);
            }
            return strSql.ToString();
        } 

    }
}
