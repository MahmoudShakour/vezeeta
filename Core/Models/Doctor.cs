using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Models
{
    public class Doctor
    {
        public string Id { get; set; }=string.Empty;
        public ApplicationUser User { get; set; }
        public int Fees { get; set; }
        public Gender Gender { get; set; }
        public List<Appointment> Appointments { get; set; }=[];
        public List<Discount> Discounts { get; set; }=[];
        public List<DoctorSpecialization> Specializations { get; set; }=[];
    }
}