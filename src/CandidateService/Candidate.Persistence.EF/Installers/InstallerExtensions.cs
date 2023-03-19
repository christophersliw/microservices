using System.Reflection;
using Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Persistence.EF.Installers;

public static class InstallerExtensions
{
    public static void InstallerPersistenceEFServiceInAssembly(this IServiceCollection services,
        IConfiguration configuration)
    {
        var installers = Assembly.GetExecutingAssembly()
            .ExportedTypes.Where(
                e => typeof(IInstaller).IsAssignableFrom(e) && !e.IsInterface && !e.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        
        installers.ForEach(installerItem => installerItem.InstallServices(services, configuration));
    }
}