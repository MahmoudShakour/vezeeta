using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration
{
    public class DoctorSpecializationConfiguration : IEntityTypeConfiguration<DoctorSpecialization>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DoctorSpecialization> builder)
        {
            builder
                .HasKey(ds => ds.Id);

            builder
                .HasIndex(ds => new { ds.SpecializationId, ds.DoctorId })
                .IsUnique();

            builder
                .HasOne(ds => ds.Doctor)
                .WithMany(ds => ds.Specializations);

            builder
                .HasOne(ds => ds.Specialization)
                .WithMany(ds => ds.Doctors);
        }
    }
}