using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsernameAsync(string username);
    }

}
