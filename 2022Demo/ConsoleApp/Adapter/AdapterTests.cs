using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Adapter
{
    public class AdapterTests
    {
        public static void TestMain()
        {


            var xiaoYezi = new XiaoYezi();
            var xiaoYeziAdapter = new XiaoYeziAdapter(xiaoYezi);
            xiaoYeziAdapter.Bark();
            xiaoYeziAdapter.Eat();

            xiaoYeziAdapter.Shepherd();
            xiaoYeziAdapter.HerdingChicken();


            Console.WriteLine("\n ---------------- \n");

            var shiYi = new ShiYi();
            xiaoYeziAdapter = new XiaoYeziAdapter(shiYi);
            xiaoYeziAdapter.Bark();
            xiaoYeziAdapter.Eat();

            xiaoYeziAdapter.Shepherd();

            xiaoYeziAdapter.HerdingChicken();


        }
        public static void TestMain_1()
        {


            var xiaoYezi = new XiaoYezi();
            var shiYiAdapter = new ShiYiAdapter(xiaoYezi);
            shiYiAdapter.Bark();
            shiYiAdapter.Eat();

            shiYiAdapter.ActingCute();
            shiYiAdapter.Demolition();

            Console.WriteLine("\n ---------------- \n");

            var shiYi = new ShiYi();
            shiYiAdapter = new ShiYiAdapter(shiYi);
            shiYiAdapter.Bark();
            shiYiAdapter.Eat();

            shiYiAdapter.ActingCute();
            shiYiAdapter.Demolition();


        }

        public static void TestMain_2()
        { 
            Console.WriteLine("******************AdapterTests-XiaoYezi***********************");


            var xiaoYezi = new XiaoYezi();
            var daHaFamilyDogAdapter = new DaHaFamilyDogAdapter(xiaoYezi);
            daHaFamilyDogAdapter.Dog.Bark();
            daHaFamilyDogAdapter.Dog.Eat();
            daHaFamilyDogAdapter.HerdingChicken();
            daHaFamilyDogAdapter.ActingCute();


            Console.WriteLine("******************AdapterTests-ShiYi*********************** \n");

            var shiYi = new ShiYi();
            daHaFamilyDogAdapter = new DaHaFamilyDogAdapter(shiYi);
            daHaFamilyDogAdapter.Dog.Bark();
            daHaFamilyDogAdapter.Dog.Eat();
            daHaFamilyDogAdapter.HerdingChicken();
            daHaFamilyDogAdapter.ActingCute();
        }
    }
}
