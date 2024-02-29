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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new ApplicationUserConfiguration().Configure(builder.Entity<ApplicationUser>());
            new DoctorConfiguration().Configure(builder.Entity<Doctor>());
            new PatientConfiguration().Configure(builder.Entity<Patient>());
            new AppointmentConfiguration().Configure(builder.Entity<Appointment>());
        }



    }
}