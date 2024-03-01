using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DoctorSpecialization
    {
        public string DoctorId { get; set; }=string.Empty;
        public int SpecializationId { get; set; }
        public Doctor Doctor { get; set; }
        public Specialization Specialization { get; set; }
    }
}