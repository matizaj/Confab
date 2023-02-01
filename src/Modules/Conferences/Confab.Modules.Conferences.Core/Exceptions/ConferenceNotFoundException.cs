using Confab.Shared.Abstractions.Exceptions;
using System.Runtime.Serialization;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    [Serializable]
    internal class ConferenceNotFoundException : ConfabException    
    {
        private Guid _id;

        public ConferenceNotFoundException(Guid id) : base($"Conerence {id} not found")
        {
            _id = id;
        }
    }
}