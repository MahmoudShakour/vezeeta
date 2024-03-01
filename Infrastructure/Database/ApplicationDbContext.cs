using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Infrastructure.Configuration;
using Infrastructure.Database.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<PatientDiscount> PatientDiscounts { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new ApplicationUserConfiguration().Configure(builder.Entity<ApplicationUser>());
            new DoctorConfiguration().Configure(builder.Entity<Doctor>());
            new PatientConfiguration().Configure(builder.Entity<Patient>());
            new AppointmentConfiguration().Configure(builder.Entity<Appointment>());
            new BookingConfiguration().Configure(builder.Entity<Booking>());
            new DiscountConfiguration().Configure(builder.Entity<Discount>());
            new PatientDiscountConfiguration().Configure(builder.Entity<PatientDiscount>());
            new SpecializationConfiguration().Configure(builder.Entity<Specialization>());
        }



    }
}