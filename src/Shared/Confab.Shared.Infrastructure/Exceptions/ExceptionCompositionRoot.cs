using Confab.Shared.Abstractions.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ExceptionCompositionRoot : IExceptionCompositionRoot
    {
        private readonly IServiceProvider _serviceProvider;

        public ExceptionCompositionRoot(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ExceptionResponse Map(Exception exception)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var mappers = scope.ServiceProvider.GetServices<IExceptionResponseMapper>().ToArray();
            var nonDefaultMappers = mappers.Where(x => x is not ExceptionToResponseMapper);
            var result = nonDefaultMappers.Select(x => x.Map(exception)).SingleOrDefault();

            if(result != null)
            {
                return result;
            }

            var defaulMapper = mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);
            return defaulMapper?.Map(exception);

        }
    }
}
