namespace Authentication.API.Contracts.V1.Responses;

public class AuthFailedReponse
{
    public IEnumerable<string> Errors { get; set; }
}