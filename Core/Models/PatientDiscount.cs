using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PatientDiscount
    {
        public string PatientId { get; set; } = string.Empty;
        public Patient Patient { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}