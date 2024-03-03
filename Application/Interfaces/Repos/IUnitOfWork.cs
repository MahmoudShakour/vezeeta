using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepo Appointments { get; }
        IBookingRepo Bookings { get; }
        IDiscountRepo Discounts { get; }
        IDoctorSpecializationRepo DoctorSpecializations { get; }
        IPatientDiscountRepo PatientDiscounts { get; }
        ISpecializationRepo Specializations { get; }
        IPatientRepo Patients { get; }
        IDoctorRepo Doctors { get; }

        Task Complete();
    }
}