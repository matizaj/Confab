using Confab.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Context
{
    internal class Context : IContext
    {
        public string RequestId { get; set; } = $"{Guid.NewGuid():N}";
        public string TraceId { get; set; }
        public IIdentityContext Identity { get; set; }
        public static Context Empty => new Context();

        internal Context()
        {
        }
        internal Context(HttpContext http):this(http.TraceIdentifier, new IdentityContext(http.User))
        {
        }

        internal Context(string traceId, IIdentityContext identity)
        {
        }

    }

    internal class IdentityContext : IIdentityContext
    {
        public Guid Id { get ; }
        public string Role { get ; }
        public Dictionary<string, IEnumerable<string>> Claims { get; }

        public bool IsAuthenticated { get; }

        public IdentityContext(ClaimsPrincipal principal)
        {
            IsAuthenticated = principal.Identity?.IsAuthenticated is true;
            Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
            Role = principal.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
            Claims = principal.Claims.GroupBy(x => x.Type).ToDictionary(x => x.Key, x => x.Select(x => x.Value.ToString()));
        }
    }
}
