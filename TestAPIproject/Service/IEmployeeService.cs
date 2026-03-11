using static TestAPIproject.ViewModels.EmployeeListViewModel;
using TestAPIproject.Models;
using TestAPIproject.ViewModels;

namespace TestAPIproject.Service
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListViewModel>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> AddAsync(Employee employee);
        Task UpdateAsync(EmployeeEditViewModel employee);
        Task DeleteAsync(int id);
    }
}
