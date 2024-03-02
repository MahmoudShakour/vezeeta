using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAppointmentRepo Appointments {get; private set;}
        public IBookingRepo Bookings {get; private set;}
        public IDiscountRepo Discounts {get; private set;}
        public IDoctorSpecializationRepo DoctorSpecializations { get; private set; }
        public IPatientDiscountRepo PatientDiscounts {get; private set;}
        public ISpecializationRepo Specializations { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            
            Appointments=new AppointmentRepo(_context);
            Bookings=new BookingRepo(_context);
            Discounts=new DiscountRepo(_context);
            DoctorSpecializations=new DoctorSpecializationRepo(_context);
            PatientDiscounts=new PatientDiscountRepo(_context);
            Specializations = new SpecializationRepo(_context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}