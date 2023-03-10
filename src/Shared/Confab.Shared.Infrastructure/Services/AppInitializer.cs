using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Confab.Shared.Infrastructure.Services
{
    internal class AppInitializer : IHostedService
    {
        private readonly IServiceProvider _service;
        private readonly ILogger<AppInitializer> _logger;

        public AppInitializer(IServiceProvider service, ILogger<AppInitializer> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x=>x.GetTypes())
                .Where(x=>typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

            using var scope = _service.CreateScope();
            foreach (var dbContetType in dbContextTypes)
            {
                var dbContext = scope.ServiceProvider.GetService(dbContetType) as DbContext;

                if (dbContext == null)
                {
                    continue;
                }

                try
                {
                    await dbContext.Database.MigrateAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
