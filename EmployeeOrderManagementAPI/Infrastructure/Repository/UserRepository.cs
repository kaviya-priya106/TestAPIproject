using Microsoft.EntityFrameworkCore;
using EmployeeOrderManagementAPI.Domain;
using EmployeeOrderManagementAPI.Infrastructure.Data;

namespace EmployeeOrderManagementAPI.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByUsernameAsync(string username)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
