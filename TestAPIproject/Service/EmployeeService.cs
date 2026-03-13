using static TestAPIproject.ViewModels.EmployeeListViewModel;
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

       
        public async Task<IEnumerable<EmployeeListViewModel>> GetAllAsync()
        {
            var employees = await _repo.GetAllAsync();
            // Manual Mapping
            return employees.Select(e => new EmployeeListViewModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Employee> AddAsync(EmployeeCreateViewModel model)
        {
      
            var employee = _mapper.Map<Employee>(model);
            /*var employee = new Employee
            {
                Name = model.Name,
                Department=model.Department,
                Role=model.Role
            };*/

            await _repo.AddAsync(employee);
            _logger.LogInformation(employee.Id, "added sccessfully");
            return employee;
        }

        public async Task UpdateAsync(EmployeeEditViewModel model)
        {
            var employee = await _repo.GetByIdAsync(model.Id);

            if (employee == null)
                throw new Exception("Employee not found");

            employee.Name = model.Name;
            employee.Salary = model.Salary;

            // Do NOT touch Department if not part of edit

            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
