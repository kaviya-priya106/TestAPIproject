using static TestAPIproject.Application.Dto.EmployeeDto;
using TestAPIproject.Application.Dto;
using TestAPIproject.Domain;
using TestAPIproject.Middleware;

namespace TestAPIproject.Application.Interfaces
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
