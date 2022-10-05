using HumanResources.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure
{
    internal class HumanResourcesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public HumanResourcesContext(DbContextOptions options)
            : base(options) { }
    }
}
        