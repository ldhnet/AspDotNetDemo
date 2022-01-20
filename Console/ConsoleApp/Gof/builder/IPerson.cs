using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.builder
{
    public interface IPerson
    {
        string Name { get; }
        int Gender { get; }
        int Age { get; }
    }
}
