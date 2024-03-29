using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Models
{
    public class Patient
    {
        public string Id { get; set; }=string.Empty;
        public ApplicationUser User { get; set; }
        public Gender Gender { get; set; }
        public List<Booking> Bookings{ get; set; }=[];
        public List<PatientDiscount> PatientDiscounts { get; set; }=[];
    }
}