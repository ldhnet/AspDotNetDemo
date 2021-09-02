using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class WordService
    {
        public void WordOpt()
        {
            var savePath = string.Format("/download/");
            var foldeName = "";//Server.MapPath(savePath);

            IList<string> wordlist = new List<string>
                    {
                        string.Format(AppDomain.CurrentDomain.BaseDirectory + "/File/Word/test01.docx"),
                        string.Format(AppDomain.CurrentDomain.BaseDirectory + "/File/Word/test02.docx")
                    };
            IDictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "{$Name1}","作者：LDH"},
                        { "{$Name2}","作者：LEELDH"}
            };

            if (!Directory.Exists(foldeName))
            {
                Directory.CreateDirectory(foldeName);
            }
            FileStream file = new FileStream($"{foldeName}/测试文档{DateTime.Now.ToString("yyyyMMddHHmmss")}.doc", FileMode.OpenOrCreate);

            var doc = WordHelper.TemplateWriteToWord(dic, wordlist);
            file.Write(doc.GetBuffer(), 0, doc.GetBuffer().Length);
            file.Close();
            file.Dispose();
        }
     
    }
}