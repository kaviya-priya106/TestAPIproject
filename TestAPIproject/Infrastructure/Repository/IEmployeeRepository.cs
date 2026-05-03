using TestAPIproject.Application.Dto;
using TestAPIproject.Domain;

namespace TestAPIproject.Infrastructure.Repository
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
