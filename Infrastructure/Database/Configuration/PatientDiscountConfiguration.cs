using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class PatientDiscountConfiguration : IEntityTypeConfiguration<PatientDiscount>
    {
        public void Configure(EntityTypeBuilder<PatientDiscount> builder)
        {
            builder
                .HasKey(pd=>new {pd.PatientId,pd.DiscountId});
            
            builder
                .HasOne(pd=>pd.Patient)
                .WithMany(p=>p.patientDiscounts)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(pd=>pd.Discount)
                .WithMany(d=>d.patientDiscounts)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder
                .Property(pd => pd.PatientId)
                .IsRequired();
            
            builder
                .Property(pd => pd.DiscountId)
                .IsRequired();
                
            builder
                .Property(pd => pd.CreatedAt)
                .IsRequired();
                
        }
    }
}