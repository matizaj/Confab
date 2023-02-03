using Confab.Modules.Conferences.Core.DAL;
using Confab.Modules.Conferences.Core.DAL.Repositories;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<ConferenceDbContext>();

            //services.AddSingleton<IHostRepository, InMemoryHostRepository>();
            services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
            services.AddScoped<IHostRepository, HostRepository>();
            services.AddTransient<IHostService, HostService>();

            //services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
            services.AddScoped<IConferenceRepository,   ConferenceRepository>();
            services.AddTransient<IConferenceService, ConferenceService>();
            services.AddSingleton<IConferencesDeletionPolicy, ConferencesDeletionPolicy>();

            return services;
        }
    }
}
