namespace Candidate.Application.Functions.Candidates.Queries.GetUserListQuery;

public class UserViewModel
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string Surrname { get; set; }
    
    public IList<ApplicationViewModel> ApplicationList { get; set; }
}