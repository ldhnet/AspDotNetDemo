using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Adapter
{
    public class RedisHelper
    {
        public RedisHelper()
        {
            Console.WriteLine($"{this.GetType().Name} 被构建");
        }
        public void AddRedis<T>()
        {
            Console.WriteLine("新增");
        }
        public void UpdateRedis<T>()
        {
            Console.WriteLine("修改");
        }
        public void DeleteRedis<T>()
        {
            Console.WriteLine("删除");
        }
    }
}
