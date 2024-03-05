using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Discount;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Application.Mappers;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenInfo? _currentUser;

        public DiscountController(IUnitOfWork unitOfWork, IJWTHelper jWTHelper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = jWTHelper.DecodeToken();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
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
                            message = "you don't have access to get discounts",
                        }
                    );
            }

            var skip = pagination.GetSkip();
            var take = pagination.GetTake();
            var discounts = await _unitOfWork.Discounts.GetAll(skip, take, d => d.DoctorId == _currentUser.Id, []);
            var discountsDto = discounts.Select(d => d.ToDiscountDto());

            return
                Ok(
                    new
                    {
                        success = true,
                        StatusCode = 200,
                        message = "discounts returned successfully",
                        data = new
                        {
                            discounts = discountsDto
                        }
                    }
                );
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

            if (_currentUser.Role != "Doctor")
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to get discount",
                        }
                    );
            }

            var discount = await _unitOfWork.Discounts.FindOne(d => d.Id == id);

            if (discount == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "discount is not found",
                        }
                    );
            }

            if (discount.DoctorId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to get discount",
                        }
                    );
            }

            var discountsDto = discount.ToDiscountDto();
            return
                Ok(
                    new
                    {
                        success = true,
                        StatusCode = 200,
                        message = "discount returned successfully",
                        data = new
                        {
                            discount = discountsDto
                        }
                    }
                );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiscountDto createDiscountDto)
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
                            message = "you don't have access to create a discount",
                        }
                    );
            }

            var discount = createDiscountDto.ToDiscount(_currentUser.Id);
            var createdDiscount = await _unitOfWork.Discounts.Create(discount);
            await _unitOfWork.Complete();

            if (createdDiscount == null)
            {
                System.Console.WriteLine("discount should be created and stored in the database");
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

            var DiscountDto = createdDiscount.ToDiscountDto();
            return
                StatusCode(
                    201,
                    new
                    {
                        success = true,
                        statusCode = 201,
                        message = "discount is created successfully",
                        data = new
                        {
                            discount = DiscountDto
                        }
                    }
                );
        }

        [HttpPost("{id}/activate")]
        public async Task<IActionResult> Activate([FromRoute] int id)
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
                            message = "you don't have access to activate this discount",
                        }
                    );
            }

            var discount = await _unitOfWork.Discounts.GetById(id);
            if (discount == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "discount is not found",
                        }
                    );
            }

            if (discount.DoctorId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to activate this discount",
                        }
                    );
            }

            var activatedDiscount = await _unitOfWork.Discounts.Activate(discount.Id);
            await _unitOfWork.Complete();

            if (activatedDiscount == null)
            {
                System.Console.WriteLine("discount should have been in the database");
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

            return Ok(
                new
                {
                    success = true,
                    StatusCode = 200,
                    message = "discount is activated successfully",
                    data = new
                    {
                        discount = activatedDiscount,
                    }
                }
            );
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
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
                            message = "you don't have access to deactivate this discount",
                        }
                    );
            }

            var discount = await _unitOfWork.Discounts.GetById(id);
            if (discount == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "discount is not found",
                        }
                    );
            }

            if (discount.DoctorId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to deactivate this discount",
                        }
                    );
            }

            var deactivatedDiscount = await _unitOfWork.Discounts.Deactivate(discount.Id);
            await _unitOfWork.Complete();

            if (deactivatedDiscount == null)
            {
                System.Console.WriteLine("discount should have been in the database");
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

            return Ok(
                new
                {
                    success = true,
                    StatusCode = 200,
                    message = "discount is activated successfully",
                    data = new
                    {
                        discount = deactivatedDiscount,
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
                            message = "you don't have access to activate this discount",
                        }
                    );
            }

            var discount = await _unitOfWork.Discounts.GetById(id);
            if (discount == null)
            {
                return
                    NotFound(
                        new
                        {
                            success = false,
                            statusCode = 404,
                            message = "discount is not found",
                        }
                    );
            }

            if (discount.DoctorId != _currentUser.Id)
            {
                return
                    StatusCode(
                        403,
                        new
                        {
                            success = false,
                            statusCode = 403,
                            message = "you don't have access to delete this discount",
                        }
                    );
            }

            _unitOfWork.Discounts.Delete(discount);
            await _unitOfWork.Complete();

            return
                StatusCode(
                    204,
                    new
                    {
                        success = true,
                        StatusCode = 204,
                        message = "discount is deleted successfully",
                    }
                );
        }

    }
}