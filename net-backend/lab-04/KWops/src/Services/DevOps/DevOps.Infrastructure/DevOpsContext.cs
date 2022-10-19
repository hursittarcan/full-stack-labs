using DevOps.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Infrastructure
{
    internal class DevOpsContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DevOpsContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DeveloperConfiguration().Configure(modelBuilder.Entity<Developer>());
            new TeamConfiguration().Configure(modelBuilder.Entity<Team>());
        }
    }
}
