using Confab.Shared.Abstractions.Modules;
using System.Reflection;

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LOadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var location = assemblies.Where(x=>!x.IsDynamic).Select(x => x.Location).ToList();

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x=>!location.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            files.ForEach(x=>assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(x=>x.GetTypes())
                .Where(x=> typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x=>x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList();
        }
       
    }
}
