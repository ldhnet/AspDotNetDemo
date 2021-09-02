using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole
{
    public static class PackData
    {
        public static void Main_test()
        {
            var invoiceList = new List<int>();
            int diffNumeber = 23;
            var alllist = new List<int>();
            for (int j = 0; j < 10; j++)
                alllist.Add(j);
            var list = alllist.OrderByDescending(c => c).ToList();

            var list_1 = GetDiffAmountInvoiceList_1(list, invoiceList, diffNumeber);


            foreach (var item in GetNumbers())
            {

                Console.WriteLine("Main process. item = " + item);

            }

        }
        public static IEnumerable<int> GetNumbers()
        {
            int diffNumeber = 20;
            // 以[0, 1, 2] 初始化数列 list
            Console.WriteLine("Initializating...");

            var invoiceList = new List<int>();
            var alllist = new List<int>();

            for (int i = 0; i < 10; i++)
                alllist.Add(i);

            var list = alllist.OrderByDescending(c => c).ToList();

            // 每次 yield return 返回一个list的数据
            Console.WriteLine("Processing...");
            for (int i = 0; i < list.Count; i++)
            {

                Console.WriteLine("Yield called.");
                yield return list[i];
            }
            Console.WriteLine("Done.");
        }

        public static List<int> GetDiffAmountInvoiceList(List<int> list, List<int> invoiceList, decimal _Amount)
        {

            if (list.First() > _Amount)
            {
                invoiceList = new List<int>();
            }
            else if (list.First() == _Amount)
            {
                invoiceList.Add(list.First());
            }
            else
            {
                var totalAmount = list.Skip(1).First() + list.First();
                if (totalAmount > _Amount)
                {
                    invoiceList.Add(list.First());
                }
                else if (totalAmount == _Amount)
                {
                    invoiceList.Add(list.First());
                    invoiceList.Add(list.Skip(1).First());
                }
                else
                {
                    invoiceList.Add(list.First());
                    invoiceList.Add(list.Skip(1).First());
                    _Amount = _Amount - totalAmount;
                    if (_Amount > 0)
                    {
                        list.Remove(list.First());
                        list.Remove(list.First());
                        return GetDiffAmountInvoiceList(list, invoiceList, _Amount);
                    }
                }
            }
            return invoiceList;
        }

        public static List<int> GetDiffAmountInvoiceList_1(List<int> list, List<int> invoiceList, decimal _Amount)
        {
            if (list.First() > _Amount)
            {
                return invoiceList;
            }
            else if (list.First() == _Amount)
            {
                invoiceList.Add(list.First());
            }
            else
            {
                invoiceList.Add(list.First());
                _Amount = _Amount - list.First();
                list.Remove(list.First());
                return GetDiffAmountInvoiceList_1(list, invoiceList, _Amount);
            }
            return invoiceList;
        }



    }
}
