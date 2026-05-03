using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmployeeOrderManagementAPI.Domain;
using EmployeeOrderManagementAPI.Middleware;
using EmployeeOrderManagementAPI.Application.Dto;
using EmployeeOrderManagementAPI.Application.Interfaces;
using EmployeeOrderManagementAPI.Application.Service;
using FluentValidation;

namespace EmployeeOrderManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class EmployeeOrdersController : ControllerBase
    {
        private IOrdersService _services;
        private readonly IValidator<PatchOrderDto> _validator;


        public EmployeeOrdersController(IOrdersService repo, IValidator<PatchOrderDto> validator)
        {
            _services = repo;
            _validator = validator;
        }

        /* [HttpGet]
         public async Task<IActionResult> GetById(int id)
         {
             return Ok(await _services.GetByIdAsync(id));
         }*/
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var orders = await _services.GetOrderByUserId(userId);
            if (orders != null)
            {
                return Ok(orders);
            }
            else
            {
                return Ok("No orders exist");

            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrders(AddOrdersDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var createdOrders = await _services.AddOrderAsync(dto,userId);
            return CreatedAtAction(nameof(GetMyOrders), new { id = createdOrders.Id }, createdOrders);

        }



        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrders(UpdateOrdersDto updateOrders,int id)
        {
            if(id!=updateOrders.Id)
                return BadRequest();

            await _services.UpdateOrdersAsync(updateOrders,id);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Updated successfully",
                Data = "Success"
            });
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> PatchOrder(int orderId, PatchOrderDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            await _services.PatchOrderAsync(orderId, userId, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            await _services.DeleteOrdersAsync(id, userId);

            return NoContent();
        }
    }
}
