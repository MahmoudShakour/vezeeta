using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Booking;
using Core.Models;

namespace Application.Mappers
{
    public static class BookingMapper
    {
        public static Booking ToBooking(this CreateBookingDto createBookingDto, string patientId)
        {
            return new Booking
            {
                AppointmentId = createBookingDto.AppointmentId,
                PatientId = patientId,
            };
        }

        public static BookingDto ToBookingDto(this Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                AppointmentId = booking.AppointmentId,
                Doctor=booking.Appointment.Doctor.ToDoctorDto(),
                Patient = booking.Patient.ToPatientDto(),
                Status = booking.Status.ToString(),
                AppointmentDate = booking.Appointment.Date,
            };
        }
    }
}