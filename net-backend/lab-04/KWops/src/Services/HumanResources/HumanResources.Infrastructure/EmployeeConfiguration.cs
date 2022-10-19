using HumanResources.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Number);
            builder.Property(e => e.Number).HasMaxLength(11)
                .HasConversion(n => n.ToString(), s => new EmployeeNumber(s));
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.FirstName).IsRequired();
        }
    }
}