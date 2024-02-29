using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int Percentage { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public Doctor Doctor { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<PatientDiscount> patientDiscounts { get; set; } = [];
    }
}