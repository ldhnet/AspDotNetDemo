using System;
using System.Text;
using System.Diagnostics;
using Serilog;
namespace SerilogConsole
{
    class Program
    {
        static void Main()
        {
            // 创建全局静态实例
            Log.Logger = new LoggerConfiguration()
                //设置最低等级
                .MinimumLevel.Verbose()
                //将事件发送到控制台并展示
                .WriteTo.Console(outputTemplate: @"{Timestamp:yyyy-MM-dd HH:mm-ss.fff }[{Level:u3}] {Message:lj}{NewLine}{Exception}")
                //将事件发送到文件
                .WriteTo.File(@".\Log\Log.txt",     // 日志文件名
                    outputTemplate:                    // 设置输出格式，显示详细异常信息
                    @"{Timestamp:yyyy-MM-dd HH:mm-ss.fff }[{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,   // 日志按月保存
                    rollOnFileSizeLimit: true,              // 限制单个文件的最大长度
                    encoding: Encoding.UTF8,                 // 文件字符编码
                    retainedFileCountLimit: 10,              // 最大保存文件数
                    fileSizeLimitBytes: 10 * 1024)                // 最大单个文件长度
                .CreateLogger();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Log.Verbose("Hello SeriLog！");
            Log.Information("开始测试");

            for (var i = 0; i < 1000; i++)  // 产生1000次异常测试
            {
                try
                {
                    Log.Debug("抛出异常");
                    throw new InvalidProgramException("程序错误。");
                }
                catch (Exception e)
                {
                    Log.Error(e, "捕获异常");
                }
            }
            stopWatch.Stop();
            Log.Information($"结束测试， 共运行{stopWatch.ElapsedMilliseconds}ms。");
            Log.CloseAndFlush();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

