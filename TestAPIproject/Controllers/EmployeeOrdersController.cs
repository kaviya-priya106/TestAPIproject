using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestAPIproject.Service;

namespace TestAPIproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeOrdersController : ControllerBase
    {
        private IEmployeeService _services;


        public EmployeeOrdersController(IEmployeeService repo)
        {
            _services = repo;
        }
        [Authorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
              var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
              if (userId != null)
             {
                  var orders = await _services.GetOrderByUserId(userId);
                  return Ok(orders);
             }
             else
            {
               return Ok("No orders exist");
            }
        }
    }
}
