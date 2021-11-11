using Framework.Utility.Extensions;
using Framework.Utility.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata; 
using System.Data.Common;
using System.Reflection;
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
            dbContext.CheckNotNull("dbContext");
            entities.CheckNotNull("entities");

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
                catch (InvalidOperationException)
                {
                    //TEntity oldEntity = dbSet.Find(entity.Id);
                    //dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
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

        public static void SetEntityDefaultValue(DbContext dbcontext)
        {
            foreach (EntityEntry entry in dbcontext.ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
            {
                #region 把null设置成对应属性类型的默认值
                Type type = entry.Entity.GetType();
                PropertyInfo[] props = ReflectionHelper.GetProperties(type);
                foreach (PropertyInfo prop in props)
                {
                    object value = prop.GetValue(entry.Entity, null);
                    if (value == null)
                    {
                        string sType = string.Empty;
                        if (prop.PropertyType.GenericTypeArguments.Length > 0)
                        {
                            sType = prop.PropertyType.GenericTypeArguments[0].Name;
                        }
                        else
                        {
                            sType = prop.PropertyType.Name;
                        }
                        switch (sType)
                        {
                            case "Boolean":
                                prop.SetValue(entry.Entity, false);
                                break;
                            case "Char":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "SByte":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Byte":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Int16":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "UInt16":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Int32":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "UInt32":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Int64":
                                prop.SetValue(entry.Entity, (Int64)0);
                                break;
                            case "UInt64":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Single":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Double":
                                prop.SetValue(entry.Entity, 0);
                                break;
                            case "Decimal":
                                prop.SetValue(entry.Entity, (decimal)0);
                                break;
                            case "DateTime":
                                prop.SetValue(entry.Entity, DefaultTime);
                                break;
                            case "String":
                                prop.SetValue(entry.Entity, string.Empty);
                                break;
                            default: break;
                        }
                    }
                    else if (value.ToString() == DateTime.MinValue.ToString())
                    {
                        // sql server datetime类型的的范围不到0001-01-01，所以转成1970-01-01
                        prop.SetValue(entry.Entity, DefaultTime);
                    }
                }
                #endregion
            }
        }

    }
}
