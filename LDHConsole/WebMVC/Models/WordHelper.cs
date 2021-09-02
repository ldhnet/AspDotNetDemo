using Aspose.Words;
using Aspose.Words.Replacing; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WordHelper
    {
        /// <summary>
        /// 替换word文档
        /// </summary>
        /// <param name="keyValuePairs">替换字典值</param>
        /// <param name="fileUrl">模板地址</param>
        /// <param name="newfileUrl">文件保存地址</param>
        /// <returns></returns>
        public static void WriteToPublicationOfResult(IDictionary<string, string> keyValuePairs, string fileUrl, string newfileUrl)
        {
            Document doc = new Document(@fileUrl);
            var extension = Path.GetExtension(newfileUrl).ToLower();
            foreach (var item in keyValuePairs)
            {
                if (item.Value == null) continue;
                doc.Range.Replace(item.Key, item.Value, new FindReplaceOptions());
            }
            var extensionSave = SaveFormat.Pdf;
            switch (extension)
            {
                case ".pdf":
                    extensionSave = SaveFormat.Pdf;
                    break;
                case ".docx":
                    extensionSave = SaveFormat.Docx;
                    break;
                case ".doc":
                    extensionSave = SaveFormat.Doc;
                    break;
                default:
                    extensionSave = SaveFormat.Docx;
                    break;
            }
            doc.Save(@newfileUrl, extensionSave);
        }

        /// <summary>
        /// 把多个word文档内容合并为一个并替换数据
        /// </summary>
        /// <param name="keyValuePairs">替换字典值</param>
        /// <param name="filelistUrl">模板地址</param>
        /// <returns></returns>
        public static MemoryStream TemplateWriteToWord(IDictionary<string, string> keyValuePairs, IList<string> filelistUrl)
        {

            Document doc = new Document(filelistUrl[0]);
            for (int i = 1; i < filelistUrl.Count; i++)
            {
                Document doci = new Document(filelistUrl[i]);
                doc.AppendDocument(doci, ImportFormatMode.KeepDifferentStyles);
            }

            foreach (var item in keyValuePairs)
            {
                if (item.Value == null) continue;
                doc.Range.Replace(item.Key, item.Value, new FindReplaceOptions());
            }
            MemoryStream ms = new MemoryStream();
            doc.Save(ms, SaveFormat.Doc);
            return ms;
        }
    }
}
