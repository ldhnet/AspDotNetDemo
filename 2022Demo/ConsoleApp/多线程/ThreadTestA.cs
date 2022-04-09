﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程
{
    public class ThreadTestA
    {
        /// <summary>
        /// 获取自增
        /// </summary>
        public static void GetIncrement()
        {
            long result = 0;
            Console.WriteLine("开始计算");
            //10个并发执行
            Parallel.For(0, 10, (i) =>
            {
                for (int j = 0; j < 10000; j++)
                {
                    result++;
                }
            });
            Console.WriteLine("结束计算");
            Console.WriteLine($"result正确值应为：{10000 * 10}");
            Console.WriteLine($"result    现值为：{result}");
            Console.ReadLine();
        }
    }
}
