using Common.Installers;

namespace Candidate.API.Installers;

public static class InstallerExtensions
{
    public static void Install<T>(this IServiceCollection services,
        IConfiguration configuration)
    {
        var installers = typeof(T).Assembly
            .ExportedTypes.Where(
                e => typeof(IInstaller).IsAssignableFrom(e) && !e.IsInterface && !e.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        
        installers.ForEach(installerItem => installerItem.InstallServices(services, configuration));
    }
}