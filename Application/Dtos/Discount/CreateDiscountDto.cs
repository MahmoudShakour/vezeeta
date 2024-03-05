using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Discount
{
    public class CreateDiscountDto
    {
        public int Percentage { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}