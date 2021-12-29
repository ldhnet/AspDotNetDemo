using AutoMapper;
using DH.Models.Dtos;
using DH.Models.Entities;
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

            //CreateMap<Employee, EmployeeDto>()
            //    .ForMember(d => d.EmployeeDetail, s => s.Ignore())
            //    .ForMember(d => d.EmployeeLogins, s => s.Ignore());
            //CreateMap<EmployeeDto, Employee>()
            //        .ForMember(d => d.EmployeeDetail, s => s.Ignore())
            //        .ForMember(d => d.EmployeeLogins, s => s.Ignore());

            CreateMap<EmployeeDetail, EmployeeDetailDto>().ReverseMap();

            CreateMap<EmployeeLogin, EmployeeLoginDto>().ReverseMap();
        }
    }
}