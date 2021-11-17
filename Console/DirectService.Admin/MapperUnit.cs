using Framework.Utility.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectService.Admin
{
    public class Foo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string CreateBy { get; set; }
    }
    [AutoInject(sourceType: typeof(Foo), targetType: typeof(FooDto))]
    public class FooDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
    }
}
