using Confab.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal static class Extensions
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlerMiddleware>();
            services.AddSingleton<IExceptionResponseMapper, ExceptionToResponseMapper>();
            services.AddSingleton<IExceptionCompositionRoot, ExceptionCompositionRoot>();

            return services;
        }

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)=>app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
