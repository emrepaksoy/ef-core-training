using AutoMapper;
using AutoMapperDemo.API.DTO;
using AutoMapperDemo.API.Entities;
using AutoMapperDemo.API.HelperFunctions;

namespace AutoMapperDemo.API.MappingProfile
{
    public class MappingProfile : Profile 
    {

        public MappingProfile()
        {
            CreateMap<User, UserReadDTO>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );


          
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserCreateDTO>();


        }
    }
}
