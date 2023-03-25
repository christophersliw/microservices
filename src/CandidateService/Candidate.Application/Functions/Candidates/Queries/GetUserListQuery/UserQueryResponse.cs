namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class UserQueryResponse
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string Surrname { get; set; }
    
    public IList<ApplicationQueryResponse> ApplicationList { get; set; }
}