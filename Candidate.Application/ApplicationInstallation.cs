using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Application
{
    public static class ApplicationInstallation
    {
        public static IServiceCollection AddCandidateApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}