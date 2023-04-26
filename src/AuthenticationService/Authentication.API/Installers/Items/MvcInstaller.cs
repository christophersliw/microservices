using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using Authentication.API.Options;
using Common.Installers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("grupa1", new OpenApiInfo {Title = "Authentication1111", Version = "v1"});
            o.SwaggerDoc("grupa2", new OpenApiInfo {Title = "Authentication1", Version = "v1111"});
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            o.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            
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
            
           // o.DocInclusionPredicate((name, api) => true);
        });



    }
}