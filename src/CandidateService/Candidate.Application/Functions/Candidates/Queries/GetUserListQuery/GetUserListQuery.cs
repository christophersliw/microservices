using MediatR;

namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class GetUserListQuery : IRequest<List<UserViewModel>>
{
  public int PageSize { get; set; }
  public int PageIndex { get; set; }
}