using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Application.Mappers;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/specializations")]
    public class SpecializationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SpecializationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specializations = await _unitOfWork.Specializations.GetAll();
            var specializationsDto = specializations.Select(s => s.ToSpecializationDto());
            return Ok(
                new
                {
                    success = true,
                    statusCode = 200,
                    message = "specializations returned successfully",
                    data = new
                    {
                        specializations = specializationsDto
                    }
                }
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var specialization = await _unitOfWork.Specializations.GetById(id);

            if (specialization == null)
            {
                return
                    NotFound(
                    new
                    {
                        success = false,
                        statusCode = 404,
                        message = "specialization is not found",
                    }
                );
            }

            var specializationDto = specialization.ToSpecializationDto();

            return Ok(
                new
                {
                    success = true,
                    statusCode = 200,
                    message = "specialization returned successfully",
                    data = new
                    {
                        specialization = specializationDto
                    }
                }
            );
        }

        [HttpGet("{id}/doctors")]
        public async Task<IActionResult> GetDoctors([FromQuery] Pagination pagination, [FromRoute] int id)
        {
            var specialization = await _unitOfWork.Specializations.GetById(id);
            if (specialization == null)
            {
                return
                    NotFound(
                    new
                    {
                        success = false,
                        statusCode = 404,
                        message = "specialization is not found",
                    }
                );
            }

            int skip = pagination.GetSkip();
            int take = pagination.GetTake();

            var doctors = await _unitOfWork.DoctorSpecializations.GetAll(skip, take, ds => ds.SpecializationId == id, ["Doctor.User"]);
            var doctorsDto = doctors.Select(d => d.ToDoctorDto()).ToList();

            return Ok(
                new
                {
                    success = true,
                    statusCode = 200,
                    message = "doctors returned successfully",
                    data = new
                    {
                        doctors = doctorsDto,
                    }
                }
            );
        }
    }
}