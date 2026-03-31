using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestAPIproject.Models;
using TestAPIproject.Service;
using TestAPIproject.ViewModels;

namespace TestAPIproject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeOrdersController : ControllerBase
    {
        private IOrdersService _services;


        public EmployeeOrdersController(IOrdersService repo)
        {
            _services = repo;
        }

        /* [HttpGet]
         public async Task<IActionResult> GetById(int id)
         {
             return Ok(await _services.GetByIdAsync(id));
         }*/
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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
        public async Task<IActionResult> CreateOrders(OrdersDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var createdOrders = await _services.AddOrderAsync(dto,userId);
            return Ok(createdOrders);

            //return CreatedAtAction( nameof(GetById), new { id = createdOrders.Id }, createdOrders);
        }



        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrders(UpdateOrders updateOrders,int id)
        {
            if(id!=updateOrders.Id)
                return BadRequest();

            await _services.UpdateOrdersAsync(updateOrders,id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _services.DeleteOrdersAsync(id);
            return NoContent();
        }
    }
}
