using System;
using System.Data;
using System.Collections.Generic;
using System.Text; 
using System.Data.SqlClient;

namespace CSharpBuilder
{
    public class sqlDbCreater : ICreator
    {
        #region 私有变量

        string mConnString = string.Empty;
        string mTableName = string.Empty;
        string mDbName = string.Empty;
        SqlConnection con = null;
        #endregion

        //public sqlDbCreater(string mConnString)
        //{ 
        //    this.mConnString = mConnString; 
        //}

        #region ICreator接口实现


        /// <summary>
        /// 测试连接数据库
        /// </summary>
        /// <param name="mConnString">链接串</param>
        public bool IsConnDB(string mConnString)
        { 
            SqlConnection conn = new SqlConnection(mConnString);
            conn.Open();
            if (conn.State == ConnectionState.Closed)
            {
                return false;
            }
            else
            { 
             return true;
            }
        }

        /// <summary>
        /// 设置数据库连接串。
        /// </summary>
        /// <param name="pConStr">链接串</param>
        public void InitConn(string pConStr)
        {
            mConnString = pConStr;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        public void ConnDB()
        {
            con = new SqlConnection(mConnString);
            con.Open();
        }

        /// <summary>
        /// 获取数据库名称列表。
        /// </summary>
        /// <returns></returns>
        public List<string> GetDatabases()
        {
            string strSQL = string.Format("select [name] from sysdatabases where name not in ('master','msdb','tempdb','model')", con.Database);
            SqlCommand com = new SqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader reader = com.ExecuteReader();
            List<string> tableList = new List<string>();
            while (reader.Read())
            {
                tableList.Add(reader[0].ToString());
            }
            reader.Close();
            return tableList;
        }

   

        /// <summary>
        /// 获取表列表。
        /// </summary>
        /// <returns></returns>
        public List<string> GetTables()
        {
            string strSQL = string.Format("SELECT NAME FROM SYSOBJECTS WHERE TYPE='U'", con.Database);
            SqlCommand com = new SqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader reader= com.ExecuteReader();
            List<string> tableList = new List<string>();
            while (reader.Read())
            {
               tableList.Add(reader[0].ToString());
            }
            reader.Close();

            return tableList;
        }

        /// <summary>
        /// 设置要生成实体的table
        /// </summary>
        /// <param name="tableName">表名</param>
        public void InitTableName(string tableName)
        {
            mTableName = tableName;
        }

        /// <summary>
        /// 构造实体类（的字符串）。
        /// </summary>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strClassName">类名称</param>
        /// <param name="strFilePath">生成类的地址</param>
        /// <returns>返回类的字符串</returns>
        public string CreateEntity(string strNameSpace,string strClassName,string strFilePath)
        {  
            string strSQL = string.Format("select column_name, data_type  FROM information_schema.columns where table_name='{0}'", mTableName);
            SqlCommand com = new SqlCommand(strSQL, con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader reader = com.ExecuteReader();
            List<string> columNames = new List<string>();
            List<string> columDesc = new List<string>();
            List<string> typeList = new List<string>();
            while (reader.Read())
            {
                columNames.Add(reader["column_name"].ToString());
                //columDesc.Add(reader["column_comment"].ToString());
                typeList.Add(GetCSharpTypeFromDbType(reader["data_type"].ToString()));
            }
            reader.Close();

            return EntityCreater.CreateEntity(mTableName,strNameSpace,strClassName,columNames.ToArray(),
                columDesc.ToArray(),typeList.ToArray());
        } 
        #endregion

        #region 私有方法
        /// <summary>
        /// 将数据库中的类型转换为c#的类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private string GetCSharpTypeFromDbType(string dbType)
        {
            switch (dbType.ToLower())
            {
                case "tinyint":
                case "smallint":
                case "mediumint":
                case "int":
                case "numeric":
                    return "int";
                case "bigint":
                    return "long";
                case "decimal":
                    return "decimal";
                case "bit":
                    return "bool";
                case "enum":
                case "set":
                case "char":
                case "varchar":
                case "nvarchar":                    
                case "text":
                    return "string";
                case "tinyblob":
                case "blob":
                case "mediumblob":
                case "longblob":
                case "binary":
                case "varbinary":
                    return "byte[]";
                case "datetime":
                    return "DateTime";
                default:
                    return dbType;
            }
        }
        #endregion
    }
}
