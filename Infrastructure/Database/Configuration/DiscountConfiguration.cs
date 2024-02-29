using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder
                .HasKey(d=>d.Id);
            
            builder
                .HasOne(d=>d.Doctor)
                .WithMany(d=>d.Discounts)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasIndex(d=>new{ d.DoctorId,d.Code })
                .IsUnique();
            
            builder 
                .Property(d=>d.Percentage)
                .IsRequired();
            
            builder 
                .Property(d=>d.DoctorId)
                .IsRequired();
            
            builder 
                .Property(d=>d.Code)
                .IsRequired();
            
            builder 
                .Property(d=>d.Status)
                .IsRequired();
        }
    }
}