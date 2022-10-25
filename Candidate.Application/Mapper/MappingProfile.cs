using AutoMapper;
using Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;
using Candidate.Domain.Entities;

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
    }
}