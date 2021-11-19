using Framework.Utility.Mapping;

namespace DH.Contracts
{
    [MapperInit(sourceType: typeof(FooDto), targetType: typeof(Foo))]
    public class Foo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string CreateBy { get; set; }
    }
    [MapperInit(sourceType: typeof(Foo), targetType: typeof(FooDto))]
    public class FooDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string CreateBy { get; set; }
    }
}