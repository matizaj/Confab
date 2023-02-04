using Confab.Modules.Conferences.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Api
{
    internal class ConferencesModule : IModule
    {
        public const string BasePath = "conferences-module";
        public string Name { get; } = "Conferences";
        public string Path { get; } = BasePath;
        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
