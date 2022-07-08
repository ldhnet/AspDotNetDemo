using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0605
{
    public  class yield_2_Test
    {
        public  void testMain()
        {
            var source=new List<int>() { 3,4,5,6,7,8,9,10,11 };
            var aa= FilterNum(source); 
        }

        /// <summary>
        /// 返回大于5的值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private  IEnumerable<int> FilterNum(IEnumerable<int> source)
        {
            foreach (int num in source)
            {
                if (num > 5)
                {
                    yield return num;
                }
            }
            yield break;
        }

        /// <summary>
        /// 普通写法 返回大于5的值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private IEnumerable<int> FilterList(IEnumerable<int> source)
        {
            List<int> numList = new List<int>();
            foreach (int num in source)
            {
                if (num > 5)
                {
                    numList.Add(num);
                }
            }
            return numList;
        }
    }
}
