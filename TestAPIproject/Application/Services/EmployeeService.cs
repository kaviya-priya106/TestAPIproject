using static TestAPIproject.Application.Dto.EmployeeDto;
using AutoMapper;
using TestAPIproject.Application.Dto;
using TestAPIproject.Infrastructure.Repository;
using TestAPIproject.Application.Interfaces;
using TestAPIproject.Domain;
using TestAPIproject.Middleware;

namespace TestAPIproject.Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository repo, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(PaginationParams pagination)
        {
            var employees = await _repo.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            return employees.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Department = e.Department
            });
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {

            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }



        public async Task<EmployeeDto> AddAsync(EmployeeCreateDto model)
        {

            var employee = _mapper.Map<Employee>(model);
            /*var employee = new Employee
            {
                Name = model.Name,
                Department=model.Department,
                Role=model.Role
            };*/

            await _repo.AddAsync(employee);
            _logger.LogInformation("Employee with Id {EmployeeId} added successfully", employee.EmployeeId);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task UpdateAsync(EmployeeEditDto model)
        {
            var employee = await _repo.GetByIdAsync(model.Id);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            employee.Name = model.Name;
            employee.UpdateSalary(model.Salary);
            employee.Department = model.Department;
            employee.Role= model.Role;
            employee.manager_id = model.Manager_Id;
            await _repo.SaveAsync();
            _logger.LogInformation("Employee with Id {EmployeeId} updated", model.Id);
        }


        public async Task PatchEmployeeAsync(int id, PatchEmployeeDto dto)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found");

            if (dto.Name != null)
                employee.Name = dto.Name;

            if (dto.Salary.HasValue)
                employee.UpdateSalary(dto.Salary.Value);


            if (dto.Department != null)
                employee.Department = dto.Department;

            if (dto.Role != null)
                employee.Role = dto.Role;

            if (dto.Manager_Id.HasValue)
                employee.manager_id = dto.Manager_Id.Value;

            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            await _repo.DeleteAsync(id);
            _logger.LogInformation("Employee with Id {EmployeeId} deleted", id);
        }


    }
}
