using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Discount;
using Core.Models;

namespace Application.Mappers
{
    public static class DiscountMapper
    {
        public static Discount ToDiscount(this CreateDiscountDto createDiscountDto, string doctorId)
        {
            return new Discount
            {
                Percentage = createDiscountDto.Percentage,
                Code = createDiscountDto.Code,
                DoctorId = doctorId,
            };
        }

        public static DiscountDto ToDiscountDto(this Discount discount)
        {
            return new DiscountDto
            {
                Id = discount.Id,
                Percentage = discount.Percentage,
                Code = discount.Code,
                DoctorId = discount.DoctorId,
                Status = discount.Status.ToString(),
            };
        }

    }
}