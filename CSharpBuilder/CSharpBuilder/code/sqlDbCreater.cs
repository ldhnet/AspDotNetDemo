using System;
using System.Data;
using System.Collections.Generic;
using System.Text; 
using System.Data.SqlClient;

namespace CSharpBuilder
{
    public class sqlDbCreater : ICreator
    {
        #region ˽�б���

        string mConnString = string.Empty;
        string mTableName = string.Empty;
        string mDbName = string.Empty;
        SqlConnection con = null;
        #endregion

        //public sqlDbCreater(string mConnString)
        //{ 
        //    this.mConnString = mConnString; 
        //}

        #region ICreator�ӿ�ʵ��


        /// <summary>
        /// �����������ݿ�
        /// </summary>
        /// <param name="mConnString">���Ӵ�</param>
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
        /// �������ݿ����Ӵ���
        /// </summary>
        /// <param name="pConStr">���Ӵ�</param>
        public void InitConn(string pConStr)
        {
            mConnString = pConStr;
        }

        /// <summary>
        /// �������ݿ�
        /// </summary>
        public void ConnDB()
        {
            con = new SqlConnection(mConnString);
            con.Open();
        }

        /// <summary>
        /// ��ȡ���ݿ������б�
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
        /// ��ȡ���б�
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
        /// ����Ҫ����ʵ���table
        /// </summary>
        /// <param name="tableName">����</param>
        public void InitTableName(string tableName)
        {
            mTableName = tableName;
        }

        /// <summary>
        /// ����ʵ���ࣨ���ַ�������
        /// </summary>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strClassName">������</param>
        /// <param name="strFilePath">������ĵ�ַ</param>
        /// <returns>��������ַ���</returns>
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

        #region ˽�з���
        /// <summary>
        /// �����ݿ��е�����ת��Ϊc#������
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
