using AutoMapper;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Security, SecurityDto>().ReverseMap();
        }
    }
}
