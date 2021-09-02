using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole
{
    public static class MailExtensions
    {
        /// <summary>
        /// 添加邮件队列静态锁
        /// </summary>
        private static readonly object MailToQueuePadlock = new object();
        public static void AddMailToQueue(this string str)
        {
            lock (MailToQueuePadlock)
            {
                var queue = new Queue<string>();
                queue.Enqueue(str);
            }
        }

        public static int t_Add(this int obj,int obt)
        {
            return obt + obj;
        }
    }
}
