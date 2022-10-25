using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using MediatR;

namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IAsyncUserRepository _asyncUserRepository;

    public GetUserListQueryHandler(IMapper mapper,  IAsyncUserRepository asyncUserRepository)
    {
        _mapper = mapper;
        _asyncUserRepository = asyncUserRepository;
    }
    
    public async Task<List<UserViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var userList = await _asyncUserRepository.GetAllAsync();

        return _mapper.Map<List<UserViewModel>>(userList);
    }
}