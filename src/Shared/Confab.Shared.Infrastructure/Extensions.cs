using Confab.Shared.Abstractions.Utility;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHostedService<AppInitializer>();
            services.AddErrorHandling();
            services.AddSingleton<IClock, UtcClock>();
            services.AddControllers()
                .ConfigureApplicationPartManager(mgr => mgr.FeatureProviders.Add(new InternalControllerFeatureProvider()));
            return services;
        }


        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandling();

            app.UseHttpsRedirection();

            app.UseAuthorization();                       

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration config, string sectioname) where T: new()
        {
            var options = new T();
            config.GetSection(sectioname).Bind(options);
            return options;
        }
    }


}
