using AutoMapper;
using AirBnb.Models;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => GetCoverImageUrl(src)))
            .ForMember(dest => dest.LandlordAvatarUrl, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src)));

        CreateMap<Location, LocationDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => new ImageDto { Url = i.Url, IsCover = i.IsCover })))
            .ForMember(dest => dest.Landlord, opt => opt.MapFrom(src => new LandlordDto { FirstName = src.Landlord.FirstName, Avatar = src.Landlord.Avatar.Url }));

        CreateMap<Location, LocationDtoV2>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => GetCoverImageUrl(src)))
            .ForMember(dest => dest.LandlordAvatarUrl, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src)))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PricePerDay));
    }

    private string GetCoverImageUrl(Location src)
    {
        var coverImage = src.Images.FirstOrDefault(i => i.IsCover);
        return coverImage != null ? coverImage.Url : null;
    }

    private string GetLandlordAvatarUrl(Location src)
    {
        return src.Landlord != null && src.Landlord.Avatar != null ? src.Landlord.Avatar.Url : null;
    }
}