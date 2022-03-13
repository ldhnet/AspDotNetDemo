using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Algorithm
{
    /*
     发出一个固定金额的红包，由若干个人来抢，需要满足哪些规则？
        1、所有人抢到金额之和等于红包金额，不能超过，也不能少于。
        2、每个人至少抢到一分钱。
        3、要保证所有人抢到金额的几率相等。
        假设剩余红包金额为M，剩余人数为N，那么有如下公式：每次抢到的金额 = 随机区间 （0， M / N × 2）

        这个公式，保证了每次随机金额的平均值是相等的，不会因为抢红包的先后顺序而造成不公平。
        举个例子：
        假设有10个人，红包总额100元。100/10×2 = 20, 
            所以第一个人的随机范围是（0，20 )，平均可以抢到10元。
        假设第一个人随机到10元，那么剩余金额是100-10 = 90 元。90/9×2 = 20, 
            所以第二个人的随机范围同样是（0，20 )，平均可以抢到10元。
        假设第二个人随机到10元，那么剩余金额是90-10 = 80 元。80/8×2 = 20, 
            所以第三个人的随机范围同样是（0，20 )，平均可以抢到10元。
        以此类推，每一次随机范围的均值是相等的。
     */
    public class RedPackage
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
            int restCash = cashCount, restPeople = peopleNumber;
            for (int i = 0; i < peopleNumber - 1; i++)
            {
                var cash = random.Next(1, restCash / restPeople * 2);
                restCash -= cash;
                restPeople--;
                redPackageList.Add(cash);
            }
            redPackageList.Add(restCash);
            return redPackageList;
        }
        /// <summary>
        /// 产生加密的随机数种子值
        /// </summary>
        /// <returns></returns>
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            //System.Security.Cryptography.RNGCryptoServiceProvider rng =  new System.Security.Cryptography.RNGCryptoServiceProvider();
            System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create(); 
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
