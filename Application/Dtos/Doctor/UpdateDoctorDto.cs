using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        public int Fees { get; set; }
        public string Gender { get; set; }=string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}