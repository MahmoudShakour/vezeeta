using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public List<DoctorSpecialization> Doctors { get; set; }
    }
}