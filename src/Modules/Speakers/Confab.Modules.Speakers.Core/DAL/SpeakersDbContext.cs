
using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Confab.Modules.Speakers.Core.DAL
{
    public class SpeakersDbContext : DbContext
    {
        public DbSet<Speaker> Speakers { get; set; }

        public SpeakersDbContext(DbContextOptions<SpeakersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("speakers");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
