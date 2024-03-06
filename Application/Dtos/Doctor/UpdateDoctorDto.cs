using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        [Required]
        [Range(0, 10000)]
        public int Fees { get; set; }
        [Required]
        [RegularExpression("^(Male|Female)$")]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}