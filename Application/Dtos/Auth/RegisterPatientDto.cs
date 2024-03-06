using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Auth
{
    public class RegisterPatientDto
    {
        [Required]
        [RegularExpression("^(Male|Female)$")]
        public string Gender { get; set; } = string.Empty;
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