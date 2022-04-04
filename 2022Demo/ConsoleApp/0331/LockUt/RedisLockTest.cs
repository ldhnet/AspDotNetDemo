using Lee.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0331.LockUt
{
    public class RedisLockTest
    { 
        //总库存
        private long nKuCuen = 0;
        //商品key名字
        private string shangpingKey = "computer_key";
        //获取锁的超时时间 秒
        private int timeout = 30 * 1000;
        private static object lockObject = new object();

        public void qiangdan()
        {
            //抢到商品的用户
            List<string> shopUsers = new List<string>();

            //构造很多用户
            List<string> users = new List<string>();

            for (int i = 1; i <= 100; i++)
            {
                users.Add("神马-" + i);
            }

            //初始化库存
            nKuCuen = 5;

            //模拟开抢 parallelStream().
            Parallel.ForEach(users, b =>
            {
                var shopUser = qiang(b);
                if (!string.IsNullOrEmpty(shopUser))
                {
                    shopUsers.Add(shopUser);
                }
            });

            shopUsers.ForEach(c =>
            {
                Console.WriteLine(c);
            });
        }

        /// <summary>
        /// 模拟抢单动作
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private String qiang(String b)
        {
            //用户开抢时间
            long startTime = DateTime.Now.Millisecond;

            //未抢到的情况下，30秒内继续获取锁
            while ((startTime + timeout) >= DateTime.Now.Millisecond)
            {
                //商品是否剩余
                if (nKuCuen <= 0)
                {
                    break;
                }
                if (CacheFactory.Cache.LockTake(shangpingKey))
                {
                    //用户b拿到锁
                    Console.WriteLine($"用户{ b }拿到锁...");
                    try
                    {
                        //商品是否剩余
                        if (nKuCuen <= 0)
                        {
                            break;
                        }
                        //模拟生成订单耗时操作，方便查看：神牛-50 多次获取锁记录
                        try
                        {
                            Thread.Sleep(1 * 1000);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        //抢购成功，商品递减，记录用户
                        nKuCuen -= 1;

                        //抢单成功跳出
                        Console.WriteLine($"用户{b}抢单成功跳出...所剩库存：{nKuCuen}");

                        return b + "抢单成功，所剩库存：" + nKuCuen;
                    }
                    finally
                    {
                        Console.WriteLine($"用户{b}释放锁...");
                        CacheFactory.Cache.LockRelease(shangpingKey); //释放锁
                    }
                }
                //else
                //{
                //    //用户b没拿到锁，在超时范围内继续请求锁，不需要处理
                //    //if (b.equals("神牛-50") || b.equals("神牛-69")) {
                //    //logger.info("用户{}等待获取锁...", b);
                //    //}
                //}
            }
            return "";//等待超时
        }
    }
}
