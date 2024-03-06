using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Booking;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Application.Mappers;
using Core.Enums;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenInfo? _currentUser;

        public BookingController(IUnitOfWork unitOfWork, IJWTHelper jWTHelper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = jWTHelper.DecodeToken();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
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

            if (_currentUser.Role != "Patient")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to get bookings",
                        }
                    );
            }

            var skip = pagination.GetSkip();
            var take = pagination.GetTake();

            var bookings = await _unitOfWork.Bookings.GetAll(skip, take, b => b.PatientId == _currentUser.Id, ["Appointment.Doctor.User", "Patient.User"]);
            var bookingsDto = bookings.Select(b => b.ToBookingDto());
            return Ok(new
            {
                success = true,
                StatusCode = 200,
                message = "bookings returned successfully",
                data = new
                {
                    bookings = bookingsDto
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
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

            if (_currentUser.Role != "Patient")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to get booking",
                        }
                    );
            }

            var booking = await _unitOfWork.Bookings.FindOne(b => b.Id == id, ["Appointment.Doctor.User", "Patient.User"]);
            if (booking == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "bookings is not found",
                        }
                    );
            }

            if (booking.PatientId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to get booking",
                        }
                    );
            }

            var bookingDto = booking.ToBookingDto();
            return
                Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "booking returned successfully",
                        data = new
                        {
                            booking = bookingDto
                        }
                    }
                );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] string? couponCode, [FromBody] CreateBookingDto createBookingDto)
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

            if (_currentUser.Role != "Patient")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to book appointments",
                        }
                    );
            }

            var appointment = await _unitOfWork.Appointments.FindOne(a => a.Id == createBookingDto.AppointmentId, ["Booking", "Doctor"]);
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

            if (appointment.Booking != null)
            {
                return
                   BadRequest(
                       new
                       {
                           success = false,
                           statusCode = 400,
                           message = "appointment is already booked",
                       }
                   );
            }

            decimal cost = appointment.Doctor.Fees;


            if (couponCode != null)
            {
                var discount = await _unitOfWork.Discounts.FindOne(d => d.Code == couponCode);
                if (discount != null && discount.Status == DiscountStatus.Activated && discount.DoctorId == appointment?.DoctorId)
                {
                    cost -= (discount.Percentage / 100.0m) * cost;
                }
            }


            var booking = createBookingDto.ToBooking(_currentUser.Id, cost);
            var createdBooking = await _unitOfWork.Bookings.Create(booking);
            await _unitOfWork.Complete();

            if (createdBooking == null)
            {
                System.Console.WriteLine("booking should have been created.");
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

            var bookingJoinPatientAndAppointment = await _unitOfWork.Bookings.FindOne(b => b.Id == createdBooking.Id, ["Appointment.Doctor.User", "Patient.User"]);
            if (bookingJoinPatientAndAppointment == null)
            {
                System.Console.WriteLine("bookingJoinPatientAndAppointment should have been created and stored in the database.");
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

            var bookingJoinPatientAndAppointmentDto = bookingJoinPatientAndAppointment.ToBookingDto();
            return
                StatusCode(
                    201,
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "appointment is booked successfully",
                        data = new
                        {
                            booking = bookingJoinPatientAndAppointmentDto
                        }
                    }
                );
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> Confirm([FromRoute] int id)
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

            if (_currentUser.Role != "Patient")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to book appointments",
                        }
                    );
            }

            var booking = await _unitOfWork.Bookings.GetById(id);
            if (booking == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "booking is not found",
                        }
                    );
            }

            if (booking.PatientId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to confirm the booking",
                        }
                    );
            }

            var ConfirmedBooking = await _unitOfWork.Bookings.Confirm(booking.Id);
            await _unitOfWork.Complete();

            if (ConfirmedBooking == null)
            {
                System.Console.WriteLine("booking should have been confirmed.");
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

            return
                Ok(
                    new
                    {
                        success = true,
                        statusCode = 200,
                        message = "booking is confirmed successfully",
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

            if (_currentUser.Role != "Patient")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to book appointments",
                        }
                    );
            }

            var booking = await _unitOfWork.Bookings.GetById(id);
            if (booking == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "booking is not found",
                        }
                    );
            }

            if (booking.PatientId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to cancel the booking",
                        }
                    );
            }

            _unitOfWork.Bookings.Delete(booking);
            await _unitOfWork.Complete();


            return
                StatusCode(
                    204,
                    new
                    {
                        success = true,
                        statusCode = 204,
                        message = "booking is cancelled successfully",
                    }
                );
        }
    }
}

// doctor token 
// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InN0cmluZyIsIlVzZXJJZCI6ImU1YmFiNmJkLTEzZTQtNDhiNi1hNDBkLThiMWQ0MWY4N2Y4NiIsIlJvbGVOYW1lIjoiRG9jdG9yIiwibmJmIjoxNzA5NTgwOTg4LCJleHAiOjE3MTIxNzI5ODgsImlhdCI6MTcwOTU4MDk4OCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjQ2IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MjQ2In0.XKH0HrKQk9BzlLmpzkOXpuL8dw7bNtFtyTE03yDlakM

// doctor token 
// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImF3cWRmZCIsIlVzZXJJZCI6Ijg4NjFjYjBhLWFjZDEtNDM2MS1hYjMyLTZmMjFlZjU0ZWY0MiIsIlJvbGVOYW1lIjoiRG9jdG9yIiwibmJmIjoxNzA5NTgwOTU0LCJleHAiOjE3MTIxNzI5NTQsImlhdCI6MTcwOTU4MDk1NCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MjQ2IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MjQ2In0.Hy2AL_I7WsrVFADySyaCnSEL70aKaTzHnNJZug6rESE

// patient token 
// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImZhZmRmZCIsIlVzZXJJZCI6ImNiM2FkM2JhLWI3ZDItNGJkZi04Y2Y0LWU3Zjk5MjI3ZTM0YiIsIlJvbGVOYW1lIjoiUGF0aWVudCIsIm5iZiI6MTcwOTU4MTE5NCwiZXhwIjoxNzEyMTczMTk0LCJpYXQiOjE3MDk1ODExOTQsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTI0NiIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTI0NiJ9.PT-FiMhdgpLGciE4VFvboVJ1Jmolsf7iA8gM762gCcY

// patient token 
// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InN0cmluZzIyMjIiLCJVc2VySWQiOiI0MjViY2UzYi0xYWFhLTQ5NjEtYTEyMi00OTFlMDYzZGM4OWIiLCJSb2xlTmFtZSI6IlBhdGllbnQiLCJuYmYiOjE3MDk1ODExMDEsImV4cCI6MTcxMjE3MzEwMSwiaWF0IjoxNzA5NTgxMTAxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyNDYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUyNDYifQ.-dr9xm_-p7v6P6hTF52D1yqWX8BSlGQL1PpK84hKiM0