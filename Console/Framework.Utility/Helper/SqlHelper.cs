using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Framework.Utility.Helper
{
    /// <summary>
    /// SqlHelper
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private string conncetionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlHelper()
        {
            conncetionString= GetConnectionString();
        }

        /// <summary>
        /// 数据库链接
        /// </summary>
        /// <param name="_conncetionString"></param>
        public SqlHelper(string _conncetionString)
        {
            if (string.IsNullOrEmpty(_conncetionString))
                conncetionString = GetConnectionString();
            else
                conncetionString = _conncetionString;
        }

        private static string GetConnectionString()
        {
            return "Server=localhost;Database=Test;User Id=sa;Password=2021@ldh;"; 
        }

        private SqlConnection connection;

        /// <summary>
        /// SqlConnection
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                string connectionString = conncetionString;
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }

        /// <summary>
        /// 批量插入数据库
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tablename"></param>
        /// <param name="columns"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public bool SqlBulkCopyToDB(DataTable data, string tablename, List<string> columns)
        {
            var result = true;
            SqlTransaction trans = Connection.BeginTransaction();
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers, trans);
            sqlBulkCopy.DestinationTableName = string.Format("[{0}]", tablename);

            foreach (var col in columns)
            {
                if (string.Equals(col, "Id", StringComparison.CurrentCultureIgnoreCase) || string.Equals(col, "Timestamp", StringComparison.CurrentCultureIgnoreCase)
                    || string.Equals(col, "RowVersion", StringComparison.CurrentCultureIgnoreCase))
                    continue;
                sqlBulkCopy.ColumnMappings.Add(col, col);
            }
            try
            {
                sqlBulkCopy.WriteToServer(data);
                trans.Commit();
            }
            catch (Exception ex)
            { 
                result = false;
                trans.Rollback();
            }
            finally
            {
                connection.Close();
                sqlBulkCopy.Close();
            }
            return result;
        }

        /// <summary>
        /// 执行一个增删改存储过程(有参)
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <param name="values">参数列表</param>
        /// <returns>影响行数</returns>
        public int ExecuteProc(string procName, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            cmd.CommandTimeout = 60;
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行一个查询无参存储过程，要关闭
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteProcSelect(string procName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteReader();
        }

        /// <summary>
        /// 执行一个带参查询存储过程,注意要关闭
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <param name="values">参数列表</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteProcSelect(string procName, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteReader();
        }

        /// <summary>
        /// 执行一个无参增删改存储过程
        /// </summary>
        /// <param name="procName">存储过程名字</param>
        /// <returns>影响行数</returns>
        public int ExecuteProc(string procName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行一个(无参)增删改语句
        /// </summary>
        /// <param name="safeSql">语句</param>
        /// <returns>影响行数</returns>
        public int ExecuteCommand(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 执行一个有参增删改操作
        /// </summary>
        /// <param name="sql">语句</param>
        /// <param name="values">参数</param>
        /// <returns>影响行数 </returns>
        public int ExecuteCommand(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 查询第一行第一列数据（无参）（返回的是什么类型就转换成什么类型）
        /// </summary>
        /// <param name="safeSql">语句</param>
        /// <returns>object</returns>
        public object GetScalar(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// 查询第一行第一列数据（有参）（返回的是什么类型就转换成什么类型）
        /// </summary>
        /// <param name="safeSql">sql</param>
        /// <param name="values">参数</param>
        /// <returns>object</returns>
        public object GetScalar(string safeSql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// 返回一个SqlDataReader（注意要关闭）
        /// </summary>
        /// <param name="safeSql">语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetReader(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            return cmd.ExecuteReader(); 
        }

        /// <summary>
        /// 返回int
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int GetScalarInt(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// 返回string
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public string GetScalarString(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return Convert.ToString(cmd.ExecuteScalar());
        }

        /// <summary>
        /// 返回一个有参SqlDataReader（注意要关闭）
        /// </summary>
        /// <param name="sql">语句</param>
        /// <param name="values">参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetReader(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// 返回一个Datatable（无参）
        /// </summary>
        /// <param name="safeSql">语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回一个Datatable（有参）
        /// </summary>
        /// <param name="sql">语句</param>
        /// <param name="values">参数</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 返回一个IList<T>（有参）
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public IList<TElement> GetEntites<TElement>(string sql, params SqlParameter[] values) where TElement : class
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0].ConvertDataTableToList<TElement>();
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            SqlTransaction tx = Connection.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (SqlException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
        }
    }
}