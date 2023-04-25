using System.Reflection;
using Common.Installers;

namespace Authentication.API.Installers;

public static class InstallerExtensions
{
    public static void Install(this IServiceCollection services,
        IConfiguration configuration)
    {
        var installers = Assembly.GetExecutingAssembly()
            .ExportedTypes.Where(
                e => typeof(IInstaller).IsAssignableFrom(e) && !e.IsInterface && !e.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        
        installers.ForEach(installerItem => installerItem.InstallServices(services, configuration));
    }
}