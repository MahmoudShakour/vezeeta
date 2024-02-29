using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string PatientId { get; set; }=string.Empty;
        public Patient Patient { get; set; }
        public string BookingStatus { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}