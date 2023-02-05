using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Contexts
{
    public interface IContext
    {
         string RequestId { get; set; }
         string TraceId { get; set; }
         IIdentityContext Identity { get; set; }
    }
}
