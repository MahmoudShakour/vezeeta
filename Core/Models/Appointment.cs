using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }=string.Empty;
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public Booking? Booking { get; set; }
    }
}