using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Recruitment.Application;

public static  class ApplicationInstallation
{
    public static IServiceCollection AddRecruitmentApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}