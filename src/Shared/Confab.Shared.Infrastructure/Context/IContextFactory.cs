using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Context
{
    internal interface IContextFactory
    {
        IContext Create();
    }

    internal class ContextFactory : IContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IContext Create()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext is null ? Context.Empty : new Context(httpContext);
        }
    }
}
