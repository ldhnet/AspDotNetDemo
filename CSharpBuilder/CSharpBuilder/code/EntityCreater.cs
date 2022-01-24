using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSharpBuilder
{
    public class EntityCreater
    {
        /// <summary>
        /// ����ʵ���ࡣ
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strNameSpace">�����ռ�����</param>
        /// <param name="strClassName">������</param>
        /// <param name="columNames">ʵ���ֶ����б�</param>
        /// <param name="columDesc">ʵ���ֶ������б�</param>
        /// <param name="typeList">�ֶ������б�</param>
        /// <returns>ʵ����Ĵ���</returns>
        public static string CreateEntity(string strTableName,string strNameSpace, string strClassName,string[] columNames, string[] columDesc, string[] typeList)
        {
            StringBuilder sb = new StringBuilder();
            //�ļ�ͷע��
            sb.AppendLine("// /************************************************************/"); 
            sb.AppendLine("// Desc: ��["+strTableName+"]��ʵ����"); 
            sb.AppendLine("// Copyright (C) 2022-2025"); 
            sb.AppendLine("// Auth: LDH");
            sb.AppendLine("// Date: " +DateTime.Now.ToString("yyyy��MM��dd��"));
            sb.AppendLine("// /************************************************************/"); 

            //Using
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;"); 
            //namespace
            sb.AppendLine("namespace " + strNameSpace);
            sb.AppendLine("{"); 
            //class desc
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// ��[" + strTableName + "]��ʵ����");
            sb.AppendLine("    /// </summary>"); 
            //class
            sb.AppendLine("    public class " + strClassName);
            sb.AppendLine("    {"); 
            //�ֶ�����
            sb.AppendLine("        #region �ֶ�����");
            for (int i = 0; i < columNames.Length; i++)
            { 
                if (columDesc != null && columDesc.Any())
                {
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine("        /// " + columDesc[i]);
                    sb.AppendLine("        /// </summary>");
                } 
                sb.AppendLine("         public " + typeList[i] + " " + columNames[i] + "{get; set;}"); 
                sb.AppendLine();
            }
            sb.AppendLine("        #endregion"); 
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        /// �����ַ������ļ�
        /// </summary>
        /// <param name="str">Ҫ������ַ���</param>
        /// <param name="filePath">�ļ�·��</param>
        public static void SaveStrToFile(string str, string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (!info.Directory.Exists)
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            StreamWriter stream =null;
            //����
            try
            {
                stream = new StreamWriter(filePath, false, Encoding.Default);
                stream.Write(str);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}
