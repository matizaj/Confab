using Confab.Shared.Abstractions.Contexts;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Context;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Modules;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        private const string CorsPolicy = "cors";
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, IList<IModule> modules)
        {
            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configurations = serviceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in configurations.AsEnumerable())
                {
                    if (!key.Contains(":module:anebled"))
                    {
                        continue;
                    }
                    if (!bool.Parse(value))
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }
            services.AddCors(cors =>
            {
                cors.AddPolicy(CorsPolicy, x =>
                {
                    x.WithOrigins("*").WithMethods("POST", "PUT", "DELETE").WithHeaders("Content-Type", "Authorization");
                });
            });
            services.AddSwaggerGen(sw =>
            {
                sw.CustomSchemaIds(x => x.FullName);
                sw.SwaggerDoc("v1", new OpenApiInfo { Title = "Confab Api", Version = "v1" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());
            services.AddModuleInfo(modules);
            services.AddHostedService<AppInitializer>();
            services.AddAuth(modules);
            services.AddErrorHandling();
            services.AddSingleton<IClock, UtcClock>();
            services.AddControllers()
                .ConfigureApplicationPartManager(mgr =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = mgr.ApplicationParts.Where(x => x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    foreach (var part in removedParts)
                    {
                        mgr.ApplicationParts.Remove(part);
                    }
                    mgr.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            return services;
        }


        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);

            app.UseErrorHandling();

            app.UseSwagger();

            app.UseReDoc(x =>
            {
                x.RoutePrefix = "docs";
                x.SpecUrl("/swagger/v1/swagger.json");
                x.DocumentTitle = "Confab Api";
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration config, string sectioname) where T : new()
        {
            var options = new T();
            config.GetSection(sectioname).Bind(options);
            return options;
        }
    }


}
