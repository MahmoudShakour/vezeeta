using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Doctor;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Application.Mappers;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("/api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenInfo? _currentUser;

        public DoctorController(IUnitOfWork unitOfWork, IJWTHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = jwtHelper.DecodeToken();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var doctorsCount = await _unitOfWork.Doctors.Count();
            return
                Ok(
                    new
                    {
                        success = true,
                        StatusCode = 200,
                        message = "doctors count returned successfully",
                        data = new
                        {
                            doctorsCount
                        }
                    }
                );
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTop([FromQuery] int numberOfDoctors)
        {
            // TODO
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var skip = pagination.GetSkip();
            var take = pagination.GetTake();
            var doctors = await _unitOfWork.Doctors.GetAll(skip, take, ["User"]);
            var doctorsDto = doctors.Select(d => d.ToDoctorDto());
            return
                Ok(
                    new
                    {
                        success = true,
                        StatusCode = 200,
                        message = "doctors returned successfully",
                        data = new
                        {
                            doctors = doctorsDto,
                        }
                    }
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] string id)
        {
            var doctor = await _unitOfWork.Doctors.FindOne(d => d.Id == id, ["User"]);
            if (doctor == null)
            {
                return
                    NotFound(
                    new
                    {
                        success = false,
                        status = 404,
                        message = "doctor is not found"
                    }
                );
            }
            var DoctorDto = doctor.ToDoctorDto();
            return
                Ok(
                new
                {
                    success = true,
                    status = 200,
                    message = "doctor returned successfully",
                    data = new
                    {
                        DoctorDto
                    }
                }
            );
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorDto updateDoctorDto)
        {
            if (_currentUser == null)
            {
                return
                    Unauthorized(
                        new
                        {
                            success = false,
                            statusCode = 401,
                            message = "you need to log in",
                        }
                    );
            }

            if (_currentUser.Role != "Doctor")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access update doctor's profile",
                        }
                    );
            }

            var doctor = updateDoctorDto.ToDoctor(_currentUser.Id);
            var updatedDoctor = await _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.Complete();
            if (updatedDoctor == null)
            {
                Console.WriteLine("doctor with current user id should have been in the database");

                return
                    StatusCode(
                        500,
                        new
                        {
                            success = false,
                            statusCode = 500,
                            message = "internal server error",
                        }
                    );
            }

            var doctorDto = updatedDoctor.ToDoctorDto();
            return
                Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "your profile is updated successfully",
                        data = new
                        {
                            doctorDto
                        }
                    }
                );

        }

    }
}