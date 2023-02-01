using Confab.Shared.Abstractions.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Time
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
}
