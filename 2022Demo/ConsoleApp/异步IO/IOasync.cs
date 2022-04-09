using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.异步IO
{
    public class IOasync
    {
        /// <summary>
        /// 异步写入文件
        /// </summary>
        /// <returns></returns>
        public async Task WriteTextAsync()
        {
            var path = "temp.txt"; //文件名
            var content = Guid.NewGuid().ToString(); //写入内容

            using (var fs = new FileStream(path,
                                            FileMode.OpenOrCreate,
                                            FileAccess.ReadWrite,
                                            FileShare.None,
                                            bufferSize: 4096,
                                            useAsync: true))
            {
                var buffer = Encoding.UTF8.GetBytes(content);
                await fs.WriteAsync(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 异步读取文本
        /// </summary>
        /// <returns></returns>
        public static async Task ReadTextAsync()
        {
            var fileName = "temp.txt"; //文件名
            using (var fs = new FileStream(fileName,
                                           FileMode.OpenOrCreate,
                                           FileAccess.Read,
                                           FileShare.None,
                                           bufferSize: 4096,
                                           useAsync: true))
            {
                var sb = new StringBuilder();
                var buffer = new byte[4096];
                var readLength = 0;

                while ((readLength = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var text = Encoding.UTF8.GetString(buffer, 0, readLength);
                    sb.Append(text);
                }

                Console.WriteLine("读取文件内容:" + sb.ToString());
            }
        }
    }
}
