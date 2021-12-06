using AutoMapper;

namespace Framework.Mapper
{
    public class MapperProfile : Profile, IProfile
    { 
        public MapperProfile() {
            CreateMap<Person, PersonDto>().ForMember(d => d.ID, s => s.Ignore());
            CreateMap<PersonDto, Person>();
         }
    }
}