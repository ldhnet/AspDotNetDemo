using System;
using System.Collections.Generic;
using System.Linq;

namespace HelperConsole
{
    public class Person
    {
        public string Name { set; get; }
        public int Age { set; get; }
        public string Gender { set; get; }
        public Dog[] Dogs { set; get; }
    }
    public class Dog
    {
        public string Name { set; get; }
    }



    public static class SelectManyHelper
    {
        private static List<Person> GetPersonList()
        {
            List<Person> personList = new List<Person>
            {
                new Person
                {
                    Name = "P1", Age = 18, Gender = "Male",
                    Dogs = new Dog[]
                    {
                        new Dog { Name = "D1" },
                        new Dog { Name = "D2" }
                    }
                },
                new Person
                {
                    Name = "P2", Age = 19, Gender = "Male",
                    Dogs = new Dog[]
                    {
                        new Dog { Name = "D3" }
                    }
                },
                new Person
                {
                    Name = "P3", Age = 17,Gender = "Female",
                    Dogs = new Dog[]
                    {
                        new Dog { Name = "D4" },
                        new Dog { Name = "D5" },
                        new Dog { Name = "D6" }
                    }
                }
            };
            return personList;
        }

        public static void FuncSelectMany_2()
        {
            var personList= GetPersonList();
            var dogs_1 = personList.SelectMany(p => p.Dogs);
            foreach (var dog in dogs_1)
            {
                Console.WriteLine($"dogs_1= {dog.Name}");
            }

            var dogs_2 = from p in personList
                       from d in p.Dogs
                       select d;

            foreach (var dog in dogs_2)
            {
                Console.WriteLine($"dogs_2= {dog.Name}");
            }

            var dogs_3 = personList.SelectMany((p, i) =>
                p.Dogs.Select(d =>
                {
                    d.Name = $"dogs_3= {i},{d.Name}";
                    return d;
                })); 

        }



        public static void FuncSelectMany()
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
            myDictionary2.Add(4, "PHP");
            myDictionary2.Add(5, "JS");

            var listDict = new List<Dictionary<int, string>>();
            listDict.Add(myDictionary);
            listDict.Add(myDictionary2);

            // var result = listDict.SelectMany(dict => dict).ToDictionary(p=>p.Key,p=>p.Value);
            var result = listDict.SelectMany(dict => dict)
                .ToLookup(t => t.Key, t => t.Value)
                .ToDictionary(p => p.Key, p => p.First());

            var result_1 = new Dictionary<int, string>();

            foreach (var dict in listDict)
            {
                foreach (var x in dict)
                {
                    result_1[x.Key] = x.Value;
                }
            }

            foreach (var item in result)
            {
                Console.WriteLine(item.Key + item.Value);
            }

            foreach (var item in result_1)
            {
                Console.WriteLine(item.Key + item.Value);
            }
        }
    }
}