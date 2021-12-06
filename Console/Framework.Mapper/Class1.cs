using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Mapper
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class PersonDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
