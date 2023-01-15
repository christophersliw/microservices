using AutoMapper;
using Recruitment.Application.Functions;
using Recruitment.Domain.Enities;

namespace Recruitment.Application.Mapper;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Offer, OfferViewModel>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId));
    }
}
