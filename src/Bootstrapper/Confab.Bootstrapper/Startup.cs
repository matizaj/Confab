using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Confab.Bootstrapper
{
    public class Startup
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<IModule> _modules;

        public Startup(IConfiguration configuration)
        {
            _assemblies = ModuleLoader.LoadAssemblies(configuration);
            _modules = ModuleLoader.LoadModules(_assemblies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(_assemblies, _modules);
            foreach (var module in _modules)
            {
                module.Register(services);
            }
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.UseInfrastructure();

            foreach (var module in _modules)
            {
                module.Use(app);
            }
            logger.LogInformation($"Module {string.Join(",", _modules.Select(x => x.Name))}");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Confab API!"));
            });

            _assemblies.Clear();
            _modules.Clear();   
        }
    }
}
