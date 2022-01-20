using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.builder
{
    public class PersonBuilder : IPersonBuilder
    {
        private readonly Person _person = new Person();

        public IPerson Build()
        {
            return _person;
        }

        public IPersonBuilder SetName(string name)
        {
            _person.Name = name;
            return this;
        }

        public IPersonBuilder SetGender(int gender)
        {
            _person.Gender = gender;
            return this;
        }

        public IPersonBuilder SetAge(int age)
        {
            _person.Age = age;
            return this;
        }
    }
}
