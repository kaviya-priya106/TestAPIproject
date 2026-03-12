using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestAPIproject.Models;
using TestAPIproject.Service;
using TestAPIproject.ViewModels;

namespace TestAPIproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _services;


        public EmployeeController(IEmployeeService repo)
        {
            _services = repo;
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
