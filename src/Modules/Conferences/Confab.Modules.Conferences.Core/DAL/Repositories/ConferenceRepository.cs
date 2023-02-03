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
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly ConferenceDbContext _context;
        private readonly DbSet<Conference> _conferences;

        public ConferenceRepository(ConferenceDbContext context)
        {
            _context = context;
            _conferences = _context.Conferences;
        }
        public async Task AddAsync(Conference conference)
        {
            await _conferences.AddAsync(conference);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Conference>> GetAll()
        {
            return await _conferences.ToListAsync();
        }

        public async  Task<Conference> GetAsync(Guid id)
        {
            return await _conferences.Include(x=>x.Host).SingleOrDefaultAsync(con => con.Id == id);
        }

        public async Task RemoveAsync(Conference conference)
        {
            _conferences.Remove(conference);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conference conference)
        {
            _conferences.Update(conference);
            await _context.SaveChangesAsync();
        }
    }
}
