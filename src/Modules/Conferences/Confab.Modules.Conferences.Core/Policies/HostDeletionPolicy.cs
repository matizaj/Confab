using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal class HostDeletionPolicy : IHostDeletionPolicy
    {
        private readonly IConferencesDeletionPolicy _conferencesDeletionPolicy;

        public HostDeletionPolicy(IConferencesDeletionPolicy conferencesDeletionPolicy)
        {
            _conferencesDeletionPolicy = conferencesDeletionPolicy;
        }
        public async Task<bool> CanDeleteAsync(Host host)
        {
           
            if(!host.Conferences.Any() || host.Conferences is null)
            {
                return true;
            }

            foreach (var conference in host.Conferences)
            {
                if(await _conferencesDeletionPolicy.CanDeleteAsync(conference) is false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}