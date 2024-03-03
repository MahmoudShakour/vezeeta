using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenInfo? _currentUser;

        public PatientController(IUnitOfWork unitOfWork, IJWTHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = jwtHelper.DecodeToken();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Count()
        {


            if (_currentUser == null)
            {
                return
                    Unauthorized(
                        new
                        {
                            message = "you need to log in",
                            statusCode = 401,
                            success = false,
                        }
                    );
            }

            if (_currentUser.Role != "Admin")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            message = "you don't have access to get patients count",
                            statusCode = 403,
                            success = false,
                        }
                    );
            }

            var patientCount = await _unitOfWork.Patients.Count();

            return Ok(
                new
                {
                    success = true,
                    StatusCode = 200,
                    message = "patients count is returned successfully",
                    data = new
                    {
                        patientCount
                    }
                }
            );
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (_currentUser == null)
            {
                return
                    Unauthorized(
                        new
                        {
                            message = "you need to log in",
                            statusCode = 401,
                            success = false,
                        }
                    );
            }

            if (_currentUser.Role != "Admin")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            message = "you don't have access to get patients count",
                            statusCode = 403,
                            success = false,
                        }
                    );
            }

            
        }
    }
}