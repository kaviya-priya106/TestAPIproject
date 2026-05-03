using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Application.Interfaces
{
    public interface IAuthService
    {
        //Task<string> LoginAsync(LoginDto dto);
        Task<string> LoginAsync(string username, string password);

        Task<string> RegisterAsync(RegisterDto dto);
    }
}
