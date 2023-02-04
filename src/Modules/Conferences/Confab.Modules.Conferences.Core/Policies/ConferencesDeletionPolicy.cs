using Confab.Modules.Conferences.Core.Entities;
using Confab.Shared.Abstractions.Time;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal class ConferencesDeletionPolicy : IConferencesDeletionPolicy
    {
        private readonly IClock _clock;

        public ConferencesDeletionPolicy(IClock clock)
        {
            _clock = clock;
        }
        public Task<bool> CanDeleteAsync(Conference conference)
        {
            // TODO check if tickets are selled or user are rigistered
            var canDelete = _clock.CurrentDate().Date.AddDays(7) < conference.From.Date;
            return Task.FromResult(canDelete); 
        }
    }
}
