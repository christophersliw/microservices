using System.Reflection;
using Common.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Installers;

public static class InstallerExtensions
{
    public static void InstallServiceInAssembly(this IServiceCollection services, IConfiguration  configuration, Type parentAssembly)
    {
        foreach (var referencedAssemblyName in  parentAssembly.Assembly.GetReferencedAssemblies())
        {
            Assembly assembly = Assembly.Load(referencedAssemblyName);
            
            var installers = assembly
                .ExportedTypes.Where(
                    e => typeof(IInstaller).IsAssignableFrom(e) && !e.IsInterface && !e.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        
            installers.ForEach(installerItem => installerItem.InstallServices(services, configuration));
        }
        
      
    }
}