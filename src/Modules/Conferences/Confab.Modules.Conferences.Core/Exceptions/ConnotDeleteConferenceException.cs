using Confab.Shared.Abstractions.Exceptions;
using System.Runtime.Serialization;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    [Serializable]
    internal class ConnotDeleteConferenceException : ConfabException
    {
        private Guid _id;

        public ConnotDeleteConferenceException(Guid id) : base($"Conferences {id} cannot be deleted")
        {
            _id = id;
        }
    }
}