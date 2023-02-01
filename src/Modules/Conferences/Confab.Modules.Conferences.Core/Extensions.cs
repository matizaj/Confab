using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IHostRepository, HostRepository>();
            services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
            services.AddTransient<IHostService, HostService>();

            services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
            services.AddTransient<IConferenceService, ConferenceService>();
            services.AddSingleton<IConferencesDeletionPolicy, ConferencesDeletionPolicy>();

            return services;
        }
    }
}
