namespace Candidate.API.Contracts.V1.Responses;

public class UserResponse
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string Surrname { get; set; }
    
    public IList<ApplicationResponse> ApplicationList { get; set; }
}