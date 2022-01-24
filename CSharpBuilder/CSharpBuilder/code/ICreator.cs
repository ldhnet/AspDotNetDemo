using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpBuilder
{
    public interface ICreator
    {
        /// <summary>
        /// 测试连接数据库
        /// </summary>
        /// <param name="mConnString">链接串</param>
        bool IsConnDB(string mConnString);
        /// <summary>
        /// 设置数据库连接串。
        /// </summary>
        /// <param name="pConStr">链接串</param>
        void InitConn(string pConStr);

        /// <summary>
        /// 连接数据库
        /// </summary>
        void ConnDB();
        /// <summary>
        /// 获取数据库名称列表。
        /// </summary>
        /// <returns></returns>
        List<string> GetDatabases();
        /// <summary>
        /// 获取表列表。
        /// </summary>
        /// <returns></returns>
        List<string> GetTables();

        /// <summary>
        /// 设置要生成实体的table
        /// </summary>
        /// <param name="tableName">表名</param>
        void InitTableName(string tableName);

        /// <summary>
        /// 构造实体类（的字符串）。
        /// </summary>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strClassName">类名称</param>
        /// <param name="strFilePath">生成类的地址</param>
        /// <returns>返回类的字符串</returns>
        string CreateEntity(string strNameSpace, string strClassName, string strFilePath);


    }
}
