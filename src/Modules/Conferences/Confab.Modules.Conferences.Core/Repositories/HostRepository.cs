using Confab.Modules.Conferences.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class HostRepository : IHostRepository
    {
        private readonly List<Host> _hosts = new();
        public Task AddAsync(Host host)
        {
            _hosts.Add(host);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<Host>> GetAll()
        {
            await Task.CompletedTask;
            return _hosts;
        }

        public Task<Host> GetAsync(Guid id)
        {
            return Task.FromResult(_hosts.SingleOrDefault(x => x.Id == id));
        }

        public Task RemoveAsync(Host host)
        {
            return Task.FromResult(_hosts.Remove(host));
        }

        public Task UpdateAsync(Host host)
        {
            return Task.CompletedTask;
        }
    }
}
