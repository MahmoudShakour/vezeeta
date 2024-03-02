using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder
                .HasKey(s => s.Id);
            
            builder
                .Property(s => s.Name)
                .IsRequired();
            
            builder
                .HasIndex(s => s.Name)
                .IsUnique();
            
        }
    }
}