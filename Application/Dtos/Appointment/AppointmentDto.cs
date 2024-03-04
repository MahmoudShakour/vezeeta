using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Doctor;
using Application.Dtos.Patient;
using Core.Models;

namespace Application.Dtos.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DoctorDto Doctor { get; set; }
        public DateTime Date { get; set; }
        public bool IsBooked { get; set; }
        public PatientDto Patient { get; set; }
    }
}