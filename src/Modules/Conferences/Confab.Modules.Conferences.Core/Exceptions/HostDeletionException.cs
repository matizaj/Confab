using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class HostDeletionException : ConfabException
    {
        public Host Host { get; }
        public HostDeletionException(Host host) : base($"Host {host.Name} cant be deleted")
        {
            Host = host;
        }      
    
    }
}
