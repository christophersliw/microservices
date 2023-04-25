using System.Security.Cryptography.Xml;
using System.Text;
using Authentication.API.Options;
using Common.Installers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Authentication.API.Installers.Items;

public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSetttings();
        configuration.Bind(nameof(JwtSetttings), jwtSettings);
        services.AddSingleton(jwtSettings);
        //
        // services.AddAuthentication(x =>
        //     {
        //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //         x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //     })
        //     .AddJwtBearer(o =>
        //     {
        //         o.SaveToken = true;
        //         o.TokenValidationParameters = new TokenValidationParameters()
        //         {
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
        //             ValidateIssuer = false,
        //             ValidateAudience = false,
        //             RequireExpirationTime = false,
        //             ValidateLifetime = true
        //         };
        //     });

        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo {Title = "Authentication", Version = "v1"});
        });



    }
}