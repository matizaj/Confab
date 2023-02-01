using Confab.Modules.Conferences.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Services
{
    internal interface IHostService
    {
        Task AddAsync(HostDto dto);
        Task<HostDetailsDto> GetAsync(Guid id);
        Task<IReadOnlyList<HostDto>> BrowseAsync();
        Task UpdateAsync(HostDetailsDto dto);
        Task RemoveAsync(Guid id);
    }
}
