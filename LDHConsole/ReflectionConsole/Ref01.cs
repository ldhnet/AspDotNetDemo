using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionConsole
{
    public static class Ref01
    {
        public class Library
        {
            /// <summary>
            /// 获取资源文件--根据资源文件键的名字，取出对应的值
            /// </summary>
            /// <param name="ResourceCode">ResourceCode</param>
            /// <returns></returns>
            //public static string GetResourceString(string ResourceCode)
            //{
            //    return WeChat_User.ResourceManager.GetString(ResourceCode);
            //}
        }

        public static void Main01()
        {
            //假设数据库中目前有2个粉丝
            List<SubscribeUserModel> list = new List<SubscribeUserModel>();
            SubscribeUserModel Model_1 = new SubscribeUserModel()
            {
                Id = 1,
                subscribe = 0,
                openid = "openid_1",
                nickname = "隔壁老王",
                sex = 1,
                language = "zh_CN",
                city = "苏州",
                province = "江苏",
                country = "中国",
                headimgurl = "headimgurl_1",
                subscribe_time = DateTime.Now.AddDays(-1)
            };

            SubscribeUserModel Model_2 = new SubscribeUserModel()
            {
                Id = 2,
                subscribe = 0,
                openid = "openid_2",
                nickname = "小磨香油",
                sex = 1,
                language = "zh_CN",
                city = "郑州",
                province = "河南",
                country = "中国",
                headimgurl = "headimgurl_2",
                subscribe_time = DateTime.Now.AddDays(-1)
            };

            list.Add(Model_1);
            list.Add(Model_2);
            //
            Type type = typeof(SubscribeUserModel);
            MemberInfo[] Properties = type.GetProperties();//得到所有公共成员
            foreach (var item in list)
            {
                Console.WriteLine("");
                Console.WriteLine("实例" + (list.IndexOf(item) + 1).ToString() + "的解析如下：");

                foreach (PropertyInfo Propertie in Properties)
                {
                    string name = Propertie.Name;
                    object value = Propertie.GetValue(item, null);
                    if (value != null)
                    {
                        Console.WriteLine(name + "：" + value.ToString());
                    }
                }
            }

            Console.ReadKey();
        }
    }

    public class WeChat_User
    {
        public int Id { get; set; }
        public int subscribe { get; set; }
        public string openid { get; set; }
    }

    public class SubscribeUserModel
    {
        public int Id { get; set; }
        public int subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public DateTime subscribe_time { get; set; }
    }
}