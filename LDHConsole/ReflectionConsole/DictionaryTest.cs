using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole
{
    public static class DictionaryTest
    {
        public static void mainDic()
        { 
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();
            myDictionary.Add(1, "C#");

            myDictionary.Add(2, "C++");

            myDictionary.Add(3, "ASP.NET");

            myDictionary.Add(4, "MVC");

            Dictionary<int, string> myDictionary2 = new Dictionary<int, string>();
            myDictionary2.Add(1, "C#");

            myDictionary2.Add(2, "C++");

            myDictionary2.Add(3, "Java");

            myDictionary2.Add(3, "PHP");

            myDictionary2.Add(5, "JS");


            var listDict = new List<Dictionary<int, string>>();

            listDict.Add(myDictionary2);

            var result = listDict.SelectMany(dict => dict).ToDictionary(p => p.Key, p => p.Value);

            foreach (var item in result)
            {
                Console.WriteLine(item.Key + item.Value);
            } 

        }
    }
}
