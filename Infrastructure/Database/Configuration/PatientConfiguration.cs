using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(p => p.Gender)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}