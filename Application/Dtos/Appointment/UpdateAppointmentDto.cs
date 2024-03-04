using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Appointment
{
    public class UpdateAppointmentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}