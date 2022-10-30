using AutoMapper;
using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using Recruitment.API.Contract.Item;

namespace Candidate.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Surrname,
            opt => opt.MapFrom(src => src.Surrname))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName));
        
        CreateMap<OfferItemResponse, OfferResponse>()
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
    }
}