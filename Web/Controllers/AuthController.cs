using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Auth;
using Application.Interfaces.Repos;
using Application.Mappers;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;

        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("/doctors/register")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterDoctorDto registerDoctorDto)
        {
            var IsDuplicate = await _authRepo.IsDuplicate(registerDoctorDto.Email, registerDoctorDto.Username);
            if (IsDuplicate)
            {
                return BadRequest(
                    new
                    {
                        success = false,
                        statusCode = 400,
                        message = "duplicate email or username"
                    }
                );
            }

            var user = registerDoctorDto.ToUser();

            var isCreated = await _authRepo.CreateDoctor(user, registerDoctorDto.Password);
            if (!isCreated)
            {
                return BadRequest(
                    new
                    {
                        success = false,
                        statusCode = 400,
                        message = "password must contain lower, upper letters, numbers, and symbols."
                    }
                );
            }

            return Created(
                    "User created successfully",
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "User created successfully."
                    }
                );

        }

        [HttpPost("/patients/register")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto registerPatientDto)
        {
            var IsDuplicate = await _authRepo.IsDuplicate(registerPatientDto.Email, registerPatientDto.Username);
            if (IsDuplicate)
            {
                return BadRequest(
                    new
                    {
                        success = false,
                        statusCode = 400,
                        message = "duplicate email or username"
                    }
                );
            }

            var user = registerPatientDto.ToUser();

            var isCreated = await _authRepo.CreatePatient(user, registerPatientDto.Password);
            if (!isCreated)
            {
                return BadRequest(
                    new
                    {
                        success = false,
                        statusCode = 400,
                        message = "password must contain lower, upper letters, numbers, and symbols."
                    }
                );
            }

            return Created(
                    "User created successfully",
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "User created successfully."
                    }
                );
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var generatedToken = await _authRepo.LoginUser(loginDto.Username, loginDto.Password);
            if (generatedToken == null)
            {
                return Unauthorized(
                    new
                    {
                        success = false,
                        statusCode = 401,
                        message = "username or password is not correct."
                    }
                );
            }

            return
                Ok(
                new
                {
                    success = true,
                    statusCode = 200,
                    message = "user is logged in successfully",
                    data = generatedToken
                }
            );
        }

    }


}