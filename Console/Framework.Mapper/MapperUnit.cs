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
        public string? Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class PersonDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public DateTime CreateTime { get; set; }
    }


    public class TestMap
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class TestMapDto
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        public DateTime CreateTime { get; set; }
    }

    //[MapperInit(sourceType: typeof(FooDto), targetType: typeof(Foo))]
    public class Foo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public decimal Money { get; set; }
        public string? CreateBy { get; set; }
    }
    public class FooDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public decimal Money { get; set; }
        public string? CreateBy { get; set; }
    }
}
