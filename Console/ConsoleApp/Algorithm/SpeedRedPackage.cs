using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Algorithm
{
    /*算法思路如下：
     *  线段分割法就是把红包总金额想象成一条线段，而每个人抢到的金额，则是这条主线段所拆分出的子线段。
        当N个人一起抢红包的时候，就需要确定N-1个切割点。
        因此，当N个人一起抢总金额为M的红包时，我们需要做N-1次随机运算，以此确定N-1个切割点。
        随机的范围区间是（1， M）。当所有切割点确定以后，子线段的长度也随之确定。这样每个人来抢红包的时候，只需要顺次领取与子线段长度等价的红包金额即可。    
    
    需要注意一下两点：
        1、每个人至少抢到一分钱。
        2、分割的线段如果重复需要重新切割
     */
    public class SpeedRedPackage
    {
        /// <summary>
        /// 产生红包数组
        /// </summary>
        /// <param name="cashCount">红包总金额，单位分</param>
        /// <param name="peopleNumber">红包人数</param>
        /// <returns></returns>
        public static List<int> DivideRedPackage(int cashCount, int peopleNumber)
        {
            List<int> redPackageList = new List<int>();
            if (cashCount <= peopleNumber)
            {
                for (int i = 0; i < cashCount; i++)
                {
                    redPackageList.Add(1);
                }
                return redPackageList;
            }

            Random random = new Random(GetRandomSeed());
            int restPeople = peopleNumber;
            List<int> lineList = new List<int>();
            while (restPeople > 1)
            {
                var line = random.Next(1, cashCount);
                if (lineList.Contains(line) == false)
                {
                    lineList.Add(line);
                    restPeople--;
                }
            }
            lineList.Sort();

            redPackageList.Add(lineList[0]);
            for (int i = 0; i < peopleNumber - 2; i++)
            {
                var cash = lineList[i + 1] - lineList[i];
                redPackageList.Add(cash);
            }
            redPackageList.Add(cashCount - lineList[lineList.Count - 1]);
            return redPackageList;
        }
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
