using AutoMapper;
using Candidate.Application.Functions.Candidates.Events;
using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;
using Candidate.Application.Responses;
using Candidate.Domain.Entities;
using Recruitment.API.Client.Responses;

namespace Candidate.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserQueryResponse>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Surrname,
            opt => opt.MapFrom(src => src.Surrname))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName));
        
        CreateMap<OfferItemResponse, OfferResponse>()
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
        
        CreateMap<CreateCandidateApplicationEvent, UserOffer>()
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId))
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId));
        
        CreateMap<OfferResponse, OfferQueryResponse>()
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
    }
}