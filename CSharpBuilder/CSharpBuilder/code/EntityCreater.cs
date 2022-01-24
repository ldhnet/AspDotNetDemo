using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSharpBuilder
{
    public class EntityCreater
    {
        /// <summary>
        /// 生成实体类。
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strNameSpace">命名空间名称</param>
        /// <param name="strClassName">类名称</param>
        /// <param name="columNames">实体字段名列表</param>
        /// <param name="columDesc">实体字段描述列表</param>
        /// <param name="typeList">字段类型列表</param>
        /// <returns>实体类的代码</returns>
        public static string CreateEntity(string strTableName,string strNameSpace, string strClassName,string[] columNames, string[] columDesc, string[] typeList)
        {
            StringBuilder sb = new StringBuilder();
            //文件头注释
            sb.AppendLine("// /************************************************************/"); 
            sb.AppendLine("// Desc: 表["+strTableName+"]的实体类"); 
            sb.AppendLine("// Copyright (C) 2022-2025"); 
            sb.AppendLine("// Auth: LDH");
            sb.AppendLine("// Date: " +DateTime.Now.ToString("yyyy年MM月dd日"));
            sb.AppendLine("// /************************************************************/"); 

            //Using
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;"); 
            //namespace
            sb.AppendLine("namespace " + strNameSpace);
            sb.AppendLine("{"); 
            //class desc
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// 表[" + strTableName + "]的实体类");
            sb.AppendLine("    /// </summary>"); 
            //class
            sb.AppendLine("    public class " + strClassName);
            sb.AppendLine("    {"); 
            //字段属性
            sb.AppendLine("        #region 字段属性");
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
        /// 保存字符串到文件
        /// </summary>
        /// <param name="str">要保存的字符串</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveStrToFile(string str, string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (!info.Directory.Exists)
            {
                Directory.CreateDirectory(info.DirectoryName);
            }
            StreamWriter stream =null;
            //保存
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
