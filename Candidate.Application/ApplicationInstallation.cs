using System.Reflection;
using Candidate.Application.BackgroundServices;
using Candidate.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Recruitment.API.Client;

namespace Candidate.Application
{
    public static class ApplicationInstallation
    {
        public static IServiceCollection AddCandidateApplication(this IServiceCollection services, Uri recruitmentServiceUri)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IOfferClientService, OfferClientService>();

            services.AddHttpClient<IOfferClient, OfferClient>(client => { client.BaseAddress = recruitmentServiceUri; });

           // services.AddHostedService<CandidateApplicationBackgroundService>();

            return services;
        }
        
        
    }
}