using AutoMapper;
using Framework.Core.Dependency;

namespace Framework.Mapper
{
    public class MapperProfile : Profile, IProfile
    { 
        public MapperProfile() {
            CreateMap<Person, PersonDto>().ForMember(d => d.ID, s => s.Ignore());
            CreateMap<PersonDto, Person>();


            CreateMap<Foo, FooDto>().ReverseMap();
            CreateMap<TestMap, TestMapDto>().ReverseMap();
        }
    }
}