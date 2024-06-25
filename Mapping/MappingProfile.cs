using AutoMapper;
using AirBnb.Models;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Location, LocationDetailsDto>()
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.Images.FirstOrDefault(img => img.IsCover).Url))
            .ForMember(dest => dest.LandlordAvatarURL, opt => opt.MapFrom(src => src.Landlord.Avatar.Url));
    }
}