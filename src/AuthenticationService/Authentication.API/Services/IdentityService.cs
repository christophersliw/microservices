using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.API.Domain;
using Authentication.API.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.API.Services;

public class IdentityService : IIdentityService
{
    private readonly JwtSetttings _jwtSetttings;

    public IdentityService(JwtSetttings jwtSetttings)
    {
        _jwtSetttings = jwtSetttings;
    }
    public async Task<AuthenticationResult> RegisterAsync(string login, string password)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSetttings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, login)
            })

        };


        return await Task.FromResult(new AuthenticationResult());
    }
}