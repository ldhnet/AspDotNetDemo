using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpBuilder
{
    public interface ICreator
    {
        /// <summary>
        /// �����������ݿ�
        /// </summary>
        /// <param name="mConnString">���Ӵ�</param>
        bool IsConnDB(string mConnString);
        /// <summary>
        /// �������ݿ����Ӵ���
        /// </summary>
        /// <param name="pConStr">���Ӵ�</param>
        void InitConn(string pConStr);

        /// <summary>
        /// �������ݿ�
        /// </summary>
        void ConnDB();
        /// <summary>
        /// ��ȡ���ݿ������б�
        /// </summary>
        /// <returns></returns>
        List<string> GetDatabases();
        /// <summary>
        /// ��ȡ���б�
        /// </summary>
        /// <returns></returns>
        List<string> GetTables();

        /// <summary>
        /// ����Ҫ����ʵ���table
        /// </summary>
        /// <param name="tableName">����</param>
        void InitTableName(string tableName);

        /// <summary>
        /// ����ʵ���ࣨ���ַ�������
        /// </summary>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strClassName">������</param>
        /// <param name="strFilePath">������ĵ�ַ</param>
        /// <returns>��������ַ���</returns>
        string CreateEntity(string strNameSpace, string strClassName, string strFilePath);


    }
}
