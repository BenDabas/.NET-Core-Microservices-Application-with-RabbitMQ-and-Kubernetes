using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(desc => desc.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(desc => desc.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(desc => desc.Publisher, opt => opt.MapFrom(src => src.Publisher));
        }
    }
}
