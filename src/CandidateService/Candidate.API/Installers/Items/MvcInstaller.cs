using System.Reflection;
using System.Text;
using Candidate.API.Options;
using Common.Installers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Candidate.API.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        });
        
        
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo {Title = "candidate API", Version = "v1"});

            var security = new Dictionary<string, IEnumerable<string>>
            {
                {"Bearer", new string[0]}

            };
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            o.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            o.TagActionsBy(api =>
            {
                // if (api.GroupName != null)
                // {
                //     return new[] { api.GroupName };
                // }
                
                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    return new[] { controllerActionDescriptor.ControllerName };
                }

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });
            
           //  o.DocInclusionPredicate((name, api) => true);
        });

    }
}