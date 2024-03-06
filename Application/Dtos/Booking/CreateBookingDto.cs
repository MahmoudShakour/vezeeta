using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Booking
{
    public class CreateBookingDto
    {
        [Required]
        public int AppointmentId { get; set; }
    }
}