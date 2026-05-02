using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestAPIproject.Application.Dto;
using TestAPIproject.Application.Interfaces;
using TestAPIproject.Middleware;

namespace TestAPIproject.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _services;


        public EmployeeController(IEmployeeService repo)
        {
            _services = repo;
        }


        
        [HttpGet("check-role")]
        public IActionResult checkRole()
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

        [HttpGet("employees")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var employees = await _services.GetAllAsync(pagination);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task< IActionResult> GetById(int id)
        {
            return Ok(await _services.GetByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDto dto)
        {

            var createdEmployee = await _services.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeEditDto model)
        {
            if (id != model.Id)
                return BadRequest();

            await _services.UpdateAsync(model);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Succeesfully Updated User information",
                Data = "Success"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _services.DeleteAsync(id);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Successfullt Deleted User information",
                Data = "Success"
            });
        }
    }
}
