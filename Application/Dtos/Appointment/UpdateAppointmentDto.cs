using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Appointment
{
    public class UpdateAppointmentDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}