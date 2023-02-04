using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Time
{
    public interface IClock
    {
        DateTime CurrentDate();
    }
}
