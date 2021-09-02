using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole
{
    class Person
    {
        public string Name;
        public void SayHi()
        {
            Console.WriteLine("Hello: {0}", this.Name);
        }
    }
    //非常简单的自定义集合（- -简单到增加，删除，索引器等功能都没有实现） 该类没有实现IEnumerable接口
    class PersonList
    {
        Person[] pers = new Person[4];
        public PersonList()
        {
            pers[0] = new Person() { Name = "1" };
            pers[1] = new Person() { Name = "2" };
            pers[2] = new Person() { Name = "3" };
            pers[3] = new Person() { Name = "4" };

        }
        //简单的迭代器方法
        public IEnumerator<Person> GetEnumerator()
        {
            foreach (Person item in pers)
            {
                //yield return 作用就是返回集合的一个元素,并移动到下一个元素上
                yield return item;
            }

        }
    }


}
