using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.多线程锁
{
    public class ReaderWriterLockTest
    {
        //读写锁， //策略支持递归
        private static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private static int index = 0;
        static void read()
        {
            try
            {
                //进入读锁
                rwl.EnterReadLock();
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},读数据,读到index:{index}");
                }
            }
            finally
            {
                //退出读锁
                rwl.ExitReadLock();
            }
        }
        static void write()
        {
            try
            {
                //尝试获写锁
                while (!rwl.TryEnterWriteLock(50))
                {
                    Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},等待写锁");
                }
                Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},获取到写锁");
                for (int i = 0; i < 5; i++)
                {
                    index++;
                    Thread.Sleep(50);
                }
                Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},写操作完成");
            }
            finally
            {
                //退出写锁
                rwl.ExitWriteLock();
            }
        }

        /// <summary>
        /// 执行多线程读写
        /// </summary>
        public static void test()
        {
            var taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
            Task[] task = new Task[6];
            task[1] = taskFactory.StartNew(write); //写
            task[0] = taskFactory.StartNew(read); //读
            task[2] = taskFactory.StartNew(read); //读
            task[3] = taskFactory.StartNew(write); //写
            task[4] = taskFactory.StartNew(read); //读
            task[5] = taskFactory.StartNew(read); //读

            for (var i = 0; i < 6; i++)
            {
                task[i].Wait();
            }

        }
    }
}
