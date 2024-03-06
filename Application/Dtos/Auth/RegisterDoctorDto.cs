using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Core.Enums;

namespace Application.Dtos.Auth
{
    public class RegisterDoctorDto
    {
        [Required]
        [Range(0,10000)]
        public int Fees { get; set; }
        [Required]
        [RegularExpression("^(Male|Female)$")]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public List<string> Specializations { get; set; } = [];
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}