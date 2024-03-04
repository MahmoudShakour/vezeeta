using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Appointment;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Application.Mappers;
using Core.Models;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenInfo? _currentUser;

        public AppointmentController(IUnitOfWork unitOfWork, IJWTHelper jWTHelper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = jWTHelper.DecodeToken();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var appointment = await _unitOfWork.Appointments.FindOne(a => a.Id == id, ["Doctor.User", "Booking"]);

            if (appointment == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "appointment is not found",
                        }
                    );
            }

            var appointmentDto = appointment.ToAppointmentDto();
            return
                Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "appointment returned successfully",
                        data = new
                        {
                            appointment = appointmentDto
                        }
                    }
                );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createAppointmentDto)
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
                            message = "you don't have access create an appointment",
                        }
                    );
            }

            var appointment = createAppointmentDto.ToAppointment(_currentUser.Id);
            var isCreated = await _unitOfWork.Appointments.Create(appointment);
            await _unitOfWork.Complete();

            if (isCreated == null)
            {
                System.Console.WriteLine("couldn't create the appointment");
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

            var appointmentWithDoctor = await _unitOfWork.Appointments.FindOne(a => a.Id == isCreated.Id, ["Doctor.User", "Booking"]);
            if (appointmentWithDoctor == null)
            {
                System.Console.WriteLine("appointment should have been created and stored in the database");
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

            var appointmentWithDoctorDto = appointmentWithDoctor.ToAppointmentDto();
            return
                StatusCode(
                    201,
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "appointment created successfully",
                        data =
                        new
                        {
                            appointment = appointmentWithDoctorDto
                        }
                    }
                );

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentDto updateAppointmentDto)
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
                            message = "you don't have access to update an appointment",
                        }
                    );
            }

            var appointment = await _unitOfWork.Appointments.GetById(updateAppointmentDto.Id);
            if (appointment == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "appointment is not found",
                        }
                    );
            }

            if (appointment.DoctorId != _currentUser.Id)
            {
                StatusCode(
                    403,
                    new
                    {
                        success = false,
                        statusCode = 403,
                        message = "you don't have access to update an appointment",
                    }
                );
            }

            var appointmentToUpdate = updateAppointmentDto.ToAppointment();
            var updatedAppoitment = await _unitOfWork.Appointments.Update(appointmentToUpdate);
            await _unitOfWork.Complete();

            if (updatedAppoitment == null)
            {
                System.Console.WriteLine("it should have been appointment with given id in the database.");
                return StatusCode(
                    500,
                    new
                    {
                        success = false,
                        statusCode = 500,
                        message = "internal server error",
                    }
                );
            }

            var appointmentWithDoctor = await _unitOfWork.Appointments.FindOne(a => a.Id == updatedAppoitment.Id, ["Doctor.User", "Booking"]);
            if (appointmentWithDoctor == null)
            {
                System.Console.WriteLine("appointment should have been updated and stored in the database");
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

            var appointmentWithDoctorDto = appointmentWithDoctor.ToAppointmentDto();
            return
                Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "appointment updated successfully",
                        data =
                        new
                        {
                            appointment = appointmentWithDoctorDto
                        }
                    }
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
                            message = "you don't have access to delete an appointment",
                        }
                    );
            }

            var appointment = await _unitOfWork.Appointments.GetById(id);

            if (appointment == null)
            {
                return
                   NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "appointment is not found",
                        }
                    );
            }

            if (appointment.DoctorId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to delete an appointment",
                        }
                    );
            }

            _unitOfWork.Appointments.Delete(appointment);
            await _unitOfWork.Complete();

            return
                StatusCode(
                    204,
                    new
                    {
                        success = true,
                        statusCode = 204,
                        message = "appointment is deleted successfully",
                    }
                );
        }
    }
}