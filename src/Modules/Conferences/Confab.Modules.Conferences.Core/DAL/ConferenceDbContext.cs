using Confab.Modules.Conferences.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.DAL
{
    internal class ConferenceDbContext:DbContext
    {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Host> Hosts { get; set; }

        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options):base(options) 
        {
        }
    }
}
