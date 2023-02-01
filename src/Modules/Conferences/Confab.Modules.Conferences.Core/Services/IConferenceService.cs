using Confab.Modules.Conferences.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Services
{
    internal interface IConferenceService
    {
        Task AddAsync(ConferenceDetailsDto dto);
        Task<ConferenceDetailsDto> GetAsync(Guid id);
        Task<IReadOnlyList<ConferencesDto>> BrowseAsync();
        Task UpdateAsync(ConferenceDetailsDto dto);
        Task RemoveAsync(Guid id);
    }
}
