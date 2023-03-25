using AutoMapper;
using Candidate.API.Contracts.V1.Requests;
using Candidate.Application.Functions.Candidates.Commands.CreateCandidateOffer;

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
    }
}