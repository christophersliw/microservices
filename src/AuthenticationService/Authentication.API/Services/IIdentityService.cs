using Authentication.API.Domain;

namespace Authentication.API.Services;

public interface IIdentityService
{
    Task<AuthenticationResult> RegisterAsync(string login, string password);
}