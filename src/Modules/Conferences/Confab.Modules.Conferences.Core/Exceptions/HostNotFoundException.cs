using Confab.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class HostNotFoundException : ConfabException
    {
        public Guid Id { get; set; }
        public HostNotFoundException(Guid id ) : base($"Host not found {id}")
        {
            Id = id;
        }
    }
}
