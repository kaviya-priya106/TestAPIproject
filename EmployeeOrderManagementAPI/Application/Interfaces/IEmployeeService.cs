using static EmployeeOrderManagementAPI.Application.Dto.EmployeeDto;
using EmployeeOrderManagementAPI.Application.Dto;
using EmployeeOrderManagementAPI.Domain;
using EmployeeOrderManagementAPI.Middleware;

namespace EmployeeOrderManagementAPI.Application.Interfaces
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
