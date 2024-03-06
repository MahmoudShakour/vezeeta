using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Discount
{
    public class CreateDiscountDto
    {
        [Required]
        [Range(1,100)]
        public int Percentage { get; set; }
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}