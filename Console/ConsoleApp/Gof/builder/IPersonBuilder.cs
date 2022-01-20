using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.builder
{
    public interface IPersonBuilder
    {
        IPerson Build();
        IPersonBuilder SetName(string name);
        IPersonBuilder SetGender(int gender);
        IPersonBuilder SetAge(int age);
    }
}
