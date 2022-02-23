using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Adapter
{
    //内置对象 实现 适配器模式 即组合模式
    public class RedisObjectHelper:RedisHelper,IHelper
    {
        //private static RedisHelper redisHelper=new RedisHelper();

        private static RedisHelper redisHelper;
        public RedisObjectHelper(RedisHelper _redisHelper)
        {
            redisHelper= _redisHelper;
        }


        public void Add<T>()
        {
            redisHelper.AddRedis<T>();
        }

        public void Delete<T>()
        {
            redisHelper.DeleteRedis<T>();
        }

        public void Update<T>()
        {
            redisHelper.UpdateRedis<T>();
        }
    }
}
