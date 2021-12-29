using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ConsoleDBTest.Helper
{
    public class SqlHelper
    {
        public T Find<T>(int id) where T : class, new()
        {
            Type type = typeof(T);
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.FindOne);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id",id)
            };
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=DHDatabase;user id=sa;password=2021@ldh"))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = new T();
                    foreach (var prop in type.GetProperties().Where(p => !Attribute.IsDefined(p, typeof(NotMappedAttribute))))
                    {
                        string propName = prop.Name;//prop.GetMappingName();
                        prop.SetValue(t, reader[prop.Name] is DBNull ? null : reader[propName]);
                    }
                    return t;
                }
            }
            return null;
        }
    }
}
