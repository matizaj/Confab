using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    public interface IHostRepository
    {
        Task AddAsync(Host host);
        Task<Host> GetAsync(Guid id);
        Task<IReadOnlyList<Host>> GetAll();
        Task UpdateAsync(Host host);
        Task RemoveAsync(Host host);
    }
}