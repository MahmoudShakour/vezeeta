using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Enums;

namespace Application.Dtos.Auth
{
    public class RegisterDoctorDto
    {
        public int Fees { get; set; }
        public string Gender { get; set; }=string.Empty;
        public List<string> Specializations { get; set; } = [];
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}