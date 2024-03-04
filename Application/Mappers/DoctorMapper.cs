using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Doctor;
using Core.Enums;
using Core.Models;

namespace Application.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorDto ToDoctorDto(this Doctor doctor)
        {
            return new DoctorDto
            {
                Id = doctor.Id,
                Fees = doctor.Fees,
                Gender = doctor.Gender.ToString(),
                PhoneNumber = doctor.User.PhoneNumber!,
                Username = doctor.User.UserName!,
                Email = doctor.User.Email!,
            };
        }
        public static Doctor ToDoctor(this UpdateDoctorDto updateDoctorDto, string id)
        {
            return new Doctor
            {
                Id = id,
                Fees = updateDoctorDto.Fees,
                Gender = Enum.Parse<Gender>(updateDoctorDto.Gender),
                User = new ApplicationUser
                {
                    PhoneNumber = updateDoctorDto.PhoneNumber,
                }
            };
        }

        public static DoctorDto ToDoctorDto(this DoctorSpecialization doctorSpecialization)
        {
            return new DoctorDto
            {
                Id = doctorSpecialization.DoctorId,
                Fees = doctorSpecialization.Doctor.Fees,
                Gender = doctorSpecialization.Doctor.Gender.ToString(),
                PhoneNumber = doctorSpecialization.Doctor.User.PhoneNumber!,
                Username = doctorSpecialization.Doctor.User.UserName!,
                Email = doctorSpecialization.Doctor.User.Email!,
            };
        }
    
    }
}