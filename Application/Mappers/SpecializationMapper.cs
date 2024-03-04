using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Specialization;
using Core.Models;

namespace Application.Mappers
{
    public static class SpecializationMapper
    {
        public static SpecializationDto ToSpecializationDto(this Specialization specialization)
        {
            return new SpecializationDto
            {
                Id = specialization.Id,
                Name = specialization.Name,
            };
        }
    }
}