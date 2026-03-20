using TestAPIproject.Models;

namespace TestAPIproject.Service
{
    public interface IAuthService
    {
        //Task<string> LoginAsync(LoginDto dto);
        Task<string> LoginAsync(string username, string password);
    }
}
