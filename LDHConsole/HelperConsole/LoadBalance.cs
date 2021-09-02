using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace HelperConsole
{
    /// <summary>
    /// 负载均衡算法
    /// </summary>
    public class LoadBalance
    {
        static int maxLinkCount;
        //
        private static object lockHelper = new object();
        /// <summary>
        /// 所有快照/服务器的权重列表 所谓权重我们可理解为最大连接数
        /// </summary>
        static List<int> snapWeightList = new List<int>() { 2, 4, 8, 12 };

        //可用的服务器权重列表
        static List<int> EnableWeightList = new List<int>() { 8, 12 };
        /// <summary>
        /// 当前的快照索引和权重信息
        /// </summary>
        static int curentSnapIndex, currentWeight, EnableWeight;
        /// <summary>
        /// 快照权重列表中最大的权重值和最大公约数
        /// </summary>
        static int maxWeight, gcd;

        static LoadBalance()
        {
            curentSnapIndex = -1;
            currentWeight = 0;
            EnableWeight = 0;
            maxWeight = GetMaxWeight(snapWeightList);
            EnableWeight = GetMaxWeight(EnableWeightList);
            gcd = GCD(snapWeightList);
            maxLinkCount = EnableWeightList.Sum();
        }
        /// <summary>
        /// 获取最大值 权重
        /// </summary>
        /// <param name="snapWeightList"></param>
        /// <returns></returns>
        public static int GetMaxWeight(List<int> snapWeightList)
        {
            int maxWeight = 0;
            foreach (int snapWeight in snapWeightList)
            {
                if (maxWeight < snapWeight)
                    maxWeight = snapWeight;
            }
            return maxWeight;
        }

        /// <summary>
        /// 获取最大公约数
        /// </summary>
        /// <param name="snapWeightList"></param>
        /// <returns></returns>
        public static int GCD(List<int> snapWeightList)
        {
            // 排序，得到数字中最小的一个 
            snapWeightList.Sort(new WeightCompare());
            int minNum = snapWeightList[0];

            // 最大公约数肯定大于等于1，且小于等于最小的那个数。 
            // 依次整除，如果余数全部为0说明是一个约数，直到打出最大的那个约数 
            int gcd = 1;
            for (int i = 1; i <= minNum; i++)
            {
                bool isFound = true;
                foreach (int snapWeight in snapWeightList)
                {
                    if (snapWeight % i != 0)
                    {
                        isFound = false;
                        break;
                    }
                }
                if (isFound)
                    gcd = i;
            }
            return gcd;
        }

        /// <summary>
        /// 权重轮询调度算法/负载均衡算法
        /// </summary>
        public static int RoundRobinScheduling()
        {
            lock (lockHelper)
            {
                while (true)
                {
                    curentSnapIndex = (curentSnapIndex + 1) % EnableWeightList.Count;
                    if (curentSnapIndex == 0)
                    {
                        currentWeight = currentWeight - gcd;
                        if (currentWeight <= 0)
                        {
                            currentWeight = maxWeight;
                            if (currentWeight == 0)
                                return -1;
                        }
                    }
                    int A = snapWeightList[curentSnapIndex];
                    if (A >= currentWeight)
                    {
                        return EnableWeightList[curentSnapIndex];
                    }
                }
            }
        }
    }

    public class WeightCompare : System.Collections.Generic.IComparer<int>
    {
        public int Compare(int weightA, int weightB)
        {
            return weightA - weightB;
        }
    }

    public class DbSnapInfo
    {
        public int SouceID { get; set; }
        public bool Enable { get; set; }
        public int Weight { get; set; }
    }
}