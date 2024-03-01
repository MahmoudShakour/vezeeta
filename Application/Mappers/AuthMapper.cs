using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Auth;
using Core.Enums;
using Core.Models;

namespace Application.Mappers
{
    public static class AuthMapper
    {
        public static ApplicationUser ToUser(this RegisterDoctorDto registerDoctorDto)
        {
            var doctor = new Doctor
            {
                Fees = registerDoctorDto.Fees,
                Gender = Enum.Parse<Gender>(registerDoctorDto.Gender),
            };

            return new ApplicationUser
            {
                UserName = registerDoctorDto.Username,
                Email = registerDoctorDto.Email,
                PhoneNumber = registerDoctorDto.PhoneNumber,
                Doctor = doctor,
            };
        }

        public static ApplicationUser ToUser(this RegisterPatientDto registerPatientDto)
        {
            var patient = new Patient
            {
                Gender = Enum.Parse<Gender>(registerPatientDto.Gender),
            };

            return new ApplicationUser
            {
                UserName = registerPatientDto.Username,
                Email = registerPatientDto.Email,
                PhoneNumber = registerPatientDto.PhoneNumber,
                Patient = patient,
            };
        }
    }
}