using AutoMapper;
using Candidate.API.Contracts.V1.Requests;
using Candidate.API.Contracts.V1.Responses;
using Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;
using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

namespace Candidate.API.Contracts.V1.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCandidateApplicationRequest, CreatedCandidateOfferCommand>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId));
        
        CreateMap<UserQueryResponse, UserResponse>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Surrname,
                opt => opt.MapFrom(src => src.Surrname))
            .ForMember(dest => dest.ApplicationList,
                opt => opt.MapFrom(src => src.ApplicationList));
        
        CreateMap<ApplicationQueryResponse, ApplicationResponse>()
            .ForMember(dest => dest.ApplicationGuid,
                opt => opt.MapFrom(src => src.ApplicationId))
            .ForMember(dest => dest.ApplicationDate,
                opt => opt.MapFrom(src => src.ApplicationDate))
            .ForMember(dest => dest.Offer,
                opt => opt.MapFrom(src => src.Offer));
        
        CreateMap<OfferQueryResponse, OfferResponse>()
            .ForMember(dest => dest.OfferId,
                opt => opt.MapFrom(src => src.OfferId))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
    }
}