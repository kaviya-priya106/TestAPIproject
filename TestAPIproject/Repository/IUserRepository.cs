using TestAPIproject.Models;

namespace TestAPIproject.Repository
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsernameAsync(string username);
    }
    
}
