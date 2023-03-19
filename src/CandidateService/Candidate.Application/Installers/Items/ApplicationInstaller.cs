using System.Reflection;
using Candidate.Application.BackgroundServices;
using Candidate.Application.Services;
using Common.Installers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recruitment.API.Client;

namespace Candidate.Application.Installers;

public class ApplicationInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IOfferClientService, OfferClientService>();

        services.AddHttpClient<IOfferClient, OfferClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["RecruitmentApiUrl"]);
            
        });

        services.AddHostedService<CandidateApplicationBackgroundService>();
    }
}