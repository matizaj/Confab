using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.DAL.Repositories
{
    internal class HostRepository : IHostRepository
    {
        private readonly ConferenceDbContext _context;
        private readonly DbSet<Host> _hosts;

        public HostRepository(ConferenceDbContext context)
        {
            _context = context;
            _hosts = _context.Hosts;
        }
        public async Task AddAsync(Host host)
        {
            await _hosts.AddAsync(host);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Host>> GetAll()
        {
            return await _hosts.ToListAsync();
        }

        public async Task<Host> GetAsync(Guid id)
        {
            return await _hosts.Include(x=>x.Conferences).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(Host host)
        {
            _hosts.Update(host);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Host host)
        {
            _hosts.Remove(host);
            await _context.SaveChangesAsync();
        }
    }
}
