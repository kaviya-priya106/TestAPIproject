using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestAPIproject.Application.Dto;
using TestAPIproject.Application.Interfaces;
using TestAPIproject.Domain;
using TestAPIproject.Middleware;

namespace TestAPIproject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _services;
        private readonly IValidator<PatchEmployeeDto> _validator;

        public EmployeeController(IEmployeeService repo, IValidator<PatchEmployeeDto> validator)
        {
            _services = repo;
             _validator= validator;
        }


        
        [HttpGet("CheckRole")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { userId, role });
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
            var employee = await _services.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }



        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto dto)
        {

            var createdEmployee = await _services.AddAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.EmployeeId },
                createdEmployee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchEmployee(int id, PatchEmployeeDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
                return BadRequest(result.Errors);
            await _services.PatchEmployeeAsync(id,dto);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeEditDto model)
        {
            if (id != model.Id)
                return BadRequest();

            await _services.UpdateAsync(model);
            return NoContent(); 
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _services.DeleteAsync(id);
            return NoContent(); 
        }
    }
}
