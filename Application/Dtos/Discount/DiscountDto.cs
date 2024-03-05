using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Doctor;

namespace Application.Dtos.Discount
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public int Percentage { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}