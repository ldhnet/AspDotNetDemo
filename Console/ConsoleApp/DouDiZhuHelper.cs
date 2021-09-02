using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DouDiZhuHelper
    {
        public void DouDiZhu()
        {
            List<string> list = new List<string>();
            Init(list);
            XiPai(list);
            Print(list);
            DiPai(list);
            list.Clear();

        }

        private void Init(List<string> list)
        {
            list.Add("大王");
            list.Add("小王");
            string[] color = new string[4] { "红桃", "黑桃", "方块", "梅花" };
            string[] cate = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", };
            for (int i = 0; i < color.Length; i++)
            {
                for (int j = 0; j < cate.Length; j++)
                {
                    list.Add(color[i] + cate[j]);
                }
            }
        }

        private void Print(List<string> list)
        {
            string[] card = list.ToArray();
            for (int i = 0; i < card.Length; i++)
            {
                if(i%17 == 0) Console.WriteLine("*********玩家A**********");

                Console.WriteLine(card[i]);
            }
            Console.ReadKey();
        }

        private void XiPai(List<string> list)
        {
            int i = list.Count;
            int j;
            if (i == 0)
            {
                return;
            }
            while (--i != 0)
            {
                Random ran = new Random();
                j = ran.Next() % (i + 1);
                string tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        private void DiPai(List<string> list)
        {
            Console.WriteLine("以下是底牌");
            Console.WriteLine("*************************");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(list[list.Count - 1]);
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
