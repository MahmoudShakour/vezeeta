using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Appointment;
using Core.Models;

namespace Application.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentDto ToAppointmentDto(this Appointment appointment)
        {
            if (appointment.Doctor != null)
            {
                return new AppointmentDto
                {
                    Id = appointment.Id,
                    Doctor = appointment.Doctor.ToDoctorDto(),
                    Date = appointment.Date,
                    IsBooked = appointment.Booking != null,
                    Patient = appointment.Booking?.Patient.ToPatientDto()!,
                };
            }

            return new AppointmentDto
            {
                Id = appointment.Id,
                Date = appointment.Date,
                IsBooked = appointment.Booking != null,
            };
        }

        public static Appointment ToAppointment(this CreateAppointmentDto createAppointmentDto, string doctorId)
        {
            return new Appointment
            {
                Date = createAppointmentDto.Date,
                DoctorId = doctorId,
            };
        }

        public static Appointment ToAppointment(this UpdateAppointmentDto updateAppointmentDto)
        {
            return new Appointment
            {
                Id = updateAppointmentDto.Id,
                Date = updateAppointmentDto.Date,
            };
        }


    }
}