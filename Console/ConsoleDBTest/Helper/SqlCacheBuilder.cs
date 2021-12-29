using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDBTest.Helper
{
    public class SqlCacheBuilder<T> where T : class, new()
    {
        public static string _InsertSql = null;
        public static string _FindOneSql = null;
        public static string _DeleteSql = null;

        /// <summary>
        /// 没有线程安全问题
        /// </summary>
        static SqlCacheBuilder()
        {
            { 
                Type type = typeof(T);
                var propertyInfos = type.GetProperties().Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute))).ToArray();//排除未映射字段
                string columString=string.Join(",", propertyInfos.Select(x=>x.Name));
                _FindOneSql = $@"Select {columString } From [{type.Name}] where Id= @Id";
            }
            {
                Type type = typeof(T);
                string columString = string.Join(",", type.GetProperties().Select(x => x.Name));
                string valuesString = string.Empty;
                _InsertSql = $@"Insert into [{type.Name}]({columString}) values ({valuesString})";
            }
            {
                Type type = typeof(T); 
                _DeleteSql = $@"Delete [{type.Name}] where Id= @Id";
            }
        }
        public static string GetSql(SqlCacheBuilderType type)
        {
            switch (type)
            {
                case SqlCacheBuilderType.FindOne:
                    return _FindOneSql; 
                case SqlCacheBuilderType.Insert:
                    return _InsertSql; 
                default:
                    throw  new Exception("Unknow SqlCacheBuilderType");
            }
        } 
    }
    public enum SqlCacheBuilderType
    {
        FindOne,
        Insert
    }
    /// <summary>
    /// 常规的字典缓存--存入+获取模式--缓存的思路是一样的
    /// 优势：方便，数据的保存以key为准
    /// 有线程安全问题
    /// </summary>
    internal class CustemCache
    {
        private static Dictionary<SqlCacheBuilderType, string> _cache = new Dictionary<SqlCacheBuilderType, string>();
        public static void Add(SqlCacheBuilderType key, string value)
        {
            _cache.Add(key, value);
        }
        public static string Get(SqlCacheBuilderType key)
        {
           return _cache[key];
        }
    }
}
