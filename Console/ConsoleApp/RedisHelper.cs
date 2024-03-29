﻿using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

public class RedisHelper
{
	static ConnectionMultiplexer redis;
	static IDatabase _db;
	static RedisValue Token = Environment.MachineName;
	public static void 并发测试()
	{
		//var options = ConfigurationOptions.Parse("localhost");
		//options.AllowAdmin = true;
		redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
		_db = redis.GetDatabase();

        //未使用锁  2个线程同时对一个数据进行加操作
        for (var i = 0; i < 2; i++)
        {
            var key = "key";
            Task.Factory.StartNew((index) =>
            {
                for (var j = 0; j < 10; j++)
                {
                    var tmp = _db.StringGet(key);
                    var data = 0;
                    int.TryParse(tmp, out data);
                    _db.StringSet(key, (data + 1).ToString(), TimeSpan.FromSeconds(5));

					Console.WriteLine("[未锁]-线程" + (Convert.ToInt32(index) + 1) + "步奏：" + j + "值为：" + _db.StringGet(key));
				}
                Console.WriteLine("[未锁]-线程" + (Convert.ToInt32(index) + 1) + "值为：" + _db.StringGet(key));
            }, i);
        }

        ////使用锁
        //for (var i = 0; i < 2; i++)
        //{
        //	Task.Factory.StartNew((index) =>
        //	{
        //		var key = "key2" + i;
        //		for (var j = 0; j < 1000; j++)
        //		{
        //			while (true)
        //			{
        //				if (StringLockToUpdate(key))
        //				{
        //					//Console.WriteLine("[StackExchange锁]-线程" + (Convert.ToInt32(index) + 1) + "步奏：" + j + "值为：" + _db.StringGet(key));
        //					break;
        //				}

        //			}
        //		}
        //		Thread.Sleep(100);//TimeSpan.FromSeconds()
        //		Console.WriteLine("[StackExchange锁]-线程" + (Convert.ToInt32(index) + 1) + "值为：" + _db.StringGet(key));
        //	}, i);
        //}

        //    //使用代码级锁
        //    for (var i = 0; i < 2; i++)
        //    {
        //        var key = "key3";
        //        Task.Factory.StartNew((index) =>
        //        {
        //            for (var k = 0; k < 100; k++)
        //            {
        //                StringLockToUpdateByNormalLock(key);

        //	Console.WriteLine("[代码级锁]-线程" + (Convert.ToInt32(index) + 1) + "步奏：" + k + "值为：" + _db.StringGet(key));
        //}
        //Thread.Sleep(100);
        //Console.WriteLine("[代码级锁]-线程" + (Convert.ToInt32(index) + 1) + "值为：" + _db.StringGet(key));
        //        }, i);
        //    }

        Console.WriteLine("完成");
		Console.ReadKey();
	}

	/// <summary>
	/// StackExchange.redis锁
	/// </summary>
	/// <param name="key">数据Key</param>
	/// <returns></returns>
	static bool StringLockToUpdate(string key)
	{
		var flag = false;
		//设置timespan避免死锁
		if (_db.LockTake("LockKey", Token, TimeSpan.FromSeconds(5)))
		{
			try
			{
				var tmp = _db.StringGet(key);
				var data = 0;
				int.TryParse(tmp, out data);
				_db.StringSet(key, data + 1, TimeSpan.FromSeconds(5));
				flag = true;
			}
			catch (Exception)
			{
				//var a = ex.Message + "\r\n" + ex.StackTrace;
				var b = string.Empty;
			}
			finally
			{
				_db.LockRelease("LockKey", Token);
			}
		}
		return flag;
	}


	static object myLock = new object();
	/// <summary>
	/// 代码级锁实现锁
	/// </summary>
	/// <param name="key">数据key</param>
	static void StringLockToUpdateByNormalLock(string key)
	{
		lock (myLock)
		{
			var tmp = _db.StringGet(key);
			var data = 0;
			int.TryParse(tmp, out data);
			_db.StringSet(key, data + 1, TimeSpan.FromSeconds(5));
		}
	} 
}