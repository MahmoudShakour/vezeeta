using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
                .HasKey(b=>b.Id);
            
            builder
                .HasOne(b=>b.Appointment)
                .WithOne(a=>a.Booking)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasIndex(b=>b.AppointmentId)
                .IsUnique();
            
            builder
                .HasOne(b=>b.Patient)
                .WithMany(p=>p.Bookings)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Property(b=>b.AppointmentId)
                .IsRequired();
            
            builder
                .Property(b=>b.PatientId)
                .IsRequired();
            
            builder
                .Property(b=>b.BookingStatus)
                .IsRequired();
            
            builder
                .Property(b=>b.CreatedAt)
                .IsRequired();

        }
    }
}