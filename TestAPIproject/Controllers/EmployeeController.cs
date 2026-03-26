using AutoMapper;
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
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _services;


        public EmployeeController(IEmployeeService repo)
        {
            _services = repo;
        }


        [Authorize]
        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "Admin")
            {
                return Ok("You're" + role);
            }
            else
            {
                return Ok("You're" + role);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _services.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task< IActionResult> GetById(int id)
        {
            return Ok(await _services.GetByIdAsync(id));
        }

        [Authorize]
        [HttpGet("my-orders")]
        public async Task< IActionResult> GetMyOrders()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var orders = await _services.GetOrderByUserId(userId);

            return Ok(orders);
        }


        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel dto)
        {

            var createdEmployee = await _services.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.Id },
                createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeEditViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            await _services.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _services.DeleteAsync(id);
            return NoContent();
        }
    }
}
