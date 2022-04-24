using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.链式编程
{
    public class UserInforMation
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public static UserInforMation Builder()
        {
            return new UserInforMation();
        }

        public UserInforMation SetNameAndAge(string name, int age)
        { 
            this.UserName = name;
            this.Age = age;
            return this;
        }
        public UserInforMation SetName(string name)
        {
            this.UserName = name; 
            return this;
        }
        public UserInforMation SetAge(int age)
        {
            this.Age = age;
            return this;
        }
        public UserInforMation OutputName()
        {
            Console.WriteLine(this.UserName);
            return this;
        }
        public UserInforMation OutputAge()
        {
            Console.WriteLine(this.Age);
            return this;
        }

    }
}
