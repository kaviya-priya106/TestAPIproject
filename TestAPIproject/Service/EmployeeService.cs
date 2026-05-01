using static TestAPIproject.ViewModels.EmployeeDto;
using TestAPIproject.Models;
using TestAPIproject.Repository;
using TestAPIproject.ViewModels;
using AutoMapper;

namespace TestAPIproject.Service
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository repo, IMapper mapper,ILogger<EmployeeService> logger)
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
                Id = e.EmployeeId,
                Name = e.Name
            });
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

     

        public async Task<Employee> AddAsync(EmployeeCreateDto model)
        {
      
            var employee = _mapper.Map<Employee>(model);
            /*var employee = new Employee
            {
                Name = model.Name,
                Department=model.Department,
                Role=model.Role
            };*/

            await _repo.AddAsync(employee);
            _logger.LogInformation(employee.EmployeeId, "added sccessfully");
            return employee;
        }

        public async Task UpdateAsync(EmployeeEditDto model)
        {
            var employee = await _repo.GetByIdAsync(model.Id);

            if (employee == null)
                throw new Exception("Employee not found");

            employee.Name = model.Name;
            employee.UpdateSalary(model.Salary);



            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

    
    }
}
