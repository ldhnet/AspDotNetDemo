using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Adapter
{
    //适配器模式
    public class RedisinhritHelper:RedisHelper,IHelper
    {
        public void Add<T>()
        { 
            base.AddRedis<T>();
        }

        public void Delete<T>()
        {
            base.DeleteRedis<T>();
        }

        public void Update<T>()
        {
            base.UpdateRedis<T>();
        }
    }
}
