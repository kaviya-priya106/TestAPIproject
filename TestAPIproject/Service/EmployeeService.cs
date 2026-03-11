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
        public EmployeeService(IEmployeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
            // Example business rule
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new Exception("Employee name cannot be empty");
            var employee = _mapper.Map<Employee>(model);
            /*var employee = new Employee
            {
                Name = model.Name,
                Department=model.Department,
                Role=model.Role
            };*/

            await _repo.AddAsync(employee);
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
