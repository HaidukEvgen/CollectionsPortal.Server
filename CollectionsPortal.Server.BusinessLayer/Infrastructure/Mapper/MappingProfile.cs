using AutoMapper;
using CollectionsPortal.Server.BusinessLayer.Models.User;
using CollectionsPortal.Server.DataLayer.Models;

namespace CollectionsPortal.Server.BusinessLayer.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
