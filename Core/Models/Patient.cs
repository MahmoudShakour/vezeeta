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
        public string Gender { get; set; }=string.Empty;
        public List<Booking> Bookings{ get; set; }=[];
    }
}