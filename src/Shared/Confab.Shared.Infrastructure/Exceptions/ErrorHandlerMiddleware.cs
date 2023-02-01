using Confab.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IExceptionCompositionRoot _mapper;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger, IExceptionCompositionRoot mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleErrorAsync(context, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception ex)
        {

            var errorResponse = _mapper.Map(ex);
            context.Response.StatusCode = (int) (errorResponse?.code ?? HttpStatusCode.InternalServerError);
            var response = errorResponse?.Response;
            if(response is null)
            {
                return;
            }
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
