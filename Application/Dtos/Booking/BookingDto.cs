using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Appointment;
using Application.Dtos.Doctor;
using Application.Dtos.Patient;
using Core.Enums;

namespace Application.Dtos.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DoctorDto Doctor { get; set; }
        public PatientDto Patient { get; set; }
        public string Status { get; set; } = BookingStatus.Pending.ToString();
        public DateTime AppointmentDate { get; set; }
    }
}