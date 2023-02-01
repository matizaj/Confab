using Confab.Modules.Conferences.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task AddAsync(Conference conference);
        Task<Conference> GetAsync(Guid id);
        Task<IReadOnlyList<Conference>> GetAll();
        Task UpdateAsync(Conference conference);
        Task RemoveAsync(Conference conference);
    }
}
