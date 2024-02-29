using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasKey(a=>a.Id);
            
            builder
                .HasOne(a=>a.Doctor)
                .WithMany(d=>d.Appointments)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Property(a=>a.DoctorId)
                .IsRequired();
            
            builder
                .Property(a=>a.Date)
                .IsRequired();
            
            builder
                .HasIndex(a=>new{a.DoctorId,a.Date})
                .IsUnique();
            

            

        }
    }
}