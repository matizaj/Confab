using Confab.Modules.Conferences.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        public Task AddAsync(Conference conference)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Conference>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Conference> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Conference conference)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Conference conference)
        {
            throw new NotImplementedException();
        }
    }
}
