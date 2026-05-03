using EmployeeOrderManagementAPI.Application.Dto;
using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Infrastructure.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(int pageNumber, int pageSize);

        Task<Employee?> GetByIdAsync(int id);

        Task AddAsync(Employee emp);

        Task SaveAsync();

        Task DeleteAsync(int id);


    }
}
