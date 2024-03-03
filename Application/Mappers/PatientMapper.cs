using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Patient;
using Core.Models;

namespace Application.Mappers
{
    public static class PatientMapper
    {
        public static PatientDto ToPatientDto(this Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                Gender = patient.Gender.ToString(),
                Username = patient.User.UserName,
                PhoneNumber = patient.User.PhoneNumber,
                Email = patient.User.Email!,
            };
        }
    }
}