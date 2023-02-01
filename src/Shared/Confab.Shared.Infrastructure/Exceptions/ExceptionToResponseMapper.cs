using Confab.Shared.Abstractions.Exceptions;
using Humanizer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ExceptionToResponseMapper : IExceptionResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new ();
        public ExceptionResponse Map(Exception exception)
        {
            return exception switch
            {
                ConfabException ex => new ExceptionResponse(new ErrorResponse(new Error(GetErrorCode(ex), ex.Message)),HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(new ErrorResponse(new Error("error", "There was an error")),HttpStatusCode.InternalServerError)
            };
        }

        private record Error(string code, string message);
        private record ErrorResponse(params Error[] errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            var code = type.Name.Underscore().Replace("_exception", string.Empty);
            return Codes.GetOrAdd(type, code);
        }

    }

}
