using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .HasOne(d => d.User)
                .WithOne(u=>u.Doctor)
                .HasForeignKey<Doctor>(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(d => d.Fees)
                .IsRequired();

            builder
                .Property(d => d.Gender)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}