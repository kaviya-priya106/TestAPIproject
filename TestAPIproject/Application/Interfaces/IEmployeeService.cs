using static TestAPIproject.Application.Dto.EmployeeDto;
using TestAPIproject.Application.Dto;
using TestAPIproject.Domain;
using TestAPIproject.Middleware;

namespace TestAPIproject.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync(PaginationParams pagination);
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<EmployeeDto> AddAsync(EmployeeCreateDto employee);
        Task UpdateAsync(EmployeeEditDto employee);

        Task PatchEmployeeAsync(int id, PatchEmployeeDto dto);
        Task DeleteAsync(int id);

    }
}
