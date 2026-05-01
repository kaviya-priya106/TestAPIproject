using static TestAPIproject.ViewModels.EmployeeDto;
using TestAPIproject.Models;
using TestAPIproject.ViewModels;

namespace TestAPIproject.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync(PaginationParams pagination);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> AddAsync(EmployeeCreateDto employee);
        Task UpdateAsync(EmployeeEditDto employee);
        Task DeleteAsync(int id);

    }
}
