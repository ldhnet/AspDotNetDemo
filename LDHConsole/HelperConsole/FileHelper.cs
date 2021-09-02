using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole
{
    public  class FileHelper
    {
        /// <summary>
        /// 异步写入文件
        /// 使用 FileStream.WriteAsync() 有2个需要注意的地方，1是要设置bufferSize，2是要将useAsync这个构造函数参数设置为true，示例调用代码如下：
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            var bits = Encoding.UTF8.GetBytes("{\"text\": \"test\"}");
            using (var fs = new FileStream(
                path: @"C:\temp\test.json",
                mode: FileMode.Create,
                access: FileAccess.Write,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                await fs.WriteAsync(bits, 0, bits.Length);
            }
        }
        static void ExistsFile(string FilePath)
        {
            //if(!File.Exists(FilePath)) 
            //File.Create(FilePath); 
            //以上写法会报错,详细解释请看下文.........
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
        }
        static void FileStreamWriter(FileInfo File)
        {
            var sw = File.AppendText();
            string[] strs = { $"Task2早上好", $"Task2下午好", $"Task2晚上好{DateTime.Now.ToString()}" };

            foreach (var s in strs)
            {
                sw.WriteLine(s);
            }
            sw.Close();
        }
        static void FileWriter()
        {
            var filePath = "c:\\temp\\tasklog.txt";
            ExistsFile(filePath);
            var d_6 = DateTime.Now.ToString("yyyy-MM-dd-HH");

            //StreamWriter sW = new StreamWriter(filePath, true, Encoding.UTF8);

            //string[] strs = { $"Task2早上好", $"Task2下午好", $"Task2晚上好{DateTime.Now.ToString()}" };
            //foreach (var s in strs)
            //{
            //    sW.WriteLine(s);
            //}
            //sW.Close();

            var filePath1 = $"c:\\temp\\log{d_6}.txt";


            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(filePath1);
                var extName = fileInfo.Extension;
                var fileName = fileInfo.Name.Substring(0, fileInfo.Name.IndexOf("."));


                // 如果文件存在
                if (fileInfo != null && fileInfo.Exists)
                {
                    FileStreamWriter(fileInfo);
                    if (true)//fileInfo.Length > 1024 * 2
                    {
                        fileInfo = new FileInfo(fileName + "-1" + extName);
                        var fl = fileInfo.Create();
                        fl.Close();
                        fl.Dispose();
                    }
                    FileStreamWriter(fileInfo);
                }
                else
                {
                    fileInfo.Create();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                // 其他处理异常的代码
            }



            var length = System.Math.Ceiling(fileInfo.Length / 1024.0) + " KB";

            Console.WriteLine("文件大小=" + System.Math.Ceiling(fileInfo.Length / 1024.0) + " KB");
        }
    }
}
