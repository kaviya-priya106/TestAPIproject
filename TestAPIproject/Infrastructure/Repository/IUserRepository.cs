using TestAPIproject.Domain;

namespace TestAPIproject.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsernameAsync(string username);
    }

}
