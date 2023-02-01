using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstractions.Exceptions
{
    public abstract class ConfabException:Exception
    {
        protected ConfabException(string message):base(message)
        {

        }
    }
}
