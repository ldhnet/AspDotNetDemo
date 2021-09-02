using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DHLibrary
{
    public class StaticHelper
    {
        private static ConcurrentDictionary<int, object> dictCache = new ConcurrentDictionary<int, object>();

        public static ConcurrentDictionary<int, object> GetProperties(int kay)
        {
            ConcurrentDictionary<int, object> properties = new ConcurrentDictionary<int, object>();
            if (dictCache.ContainsKey(kay))
            {
                properties = dictCache;
            }
            else
            {
                dictCache.TryAdd(kay, kay);
                properties = dictCache;
            }

            return properties;
        }
        //          for (int i = 0; i< 5; i++)
        //    {
        //      var a2a = StaticHelper.GetProperties(i);
        //Console.WriteLine(a2a.Keys.ToArray()[i].ToString() + "是" + a2a.Values.ToArray()[i].ToString());
        //        Console.WriteLine("请输入任意建");
        //        Console.ReadKey();
                
        //    }

}
}
