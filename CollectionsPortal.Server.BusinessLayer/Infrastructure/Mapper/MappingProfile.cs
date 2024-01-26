using AutoMapper;
using CollectionsPortal.Server.BusinessLayer.Models.Collection;
using CollectionsPortal.Server.BusinessLayer.Models.Item;
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

            CreateMap<CollectionDto, Collection>();

            CreateMap<Collection, CollectionDto>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.UserName));

            CreateMap<CollectionCategory, CategoryDto>().ReverseMap();
            
            CreateMap<NewCollectionDto, Collection>();

            CreateMap<CollectionItem, ItemDto>().ReverseMap();

            CreateMap<ItemTag, ItemTagDto>().ReverseMap();

            CreateMap<NewItemDto, CollectionItem>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(_ => new List<ItemTag>()));

            CreateMap<CollectionItem, ItemGeneralDto>()
                .ForMember(dest => dest.CollectionName, opt => opt.MapFrom(src => src.Collection.Name))
                .ForMember(dest => dest.CollectionId, opt => opt.MapFrom(src => src.Collection.Id))
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Collection.Creator.UserName));

        }
    }
}
