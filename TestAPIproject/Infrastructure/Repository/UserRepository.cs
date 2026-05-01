using Microsoft.EntityFrameworkCore;
using TestAPIproject.Domain;
using TestAPIproject.Infrastructure.Data;

namespace TestAPIproject.Infrastructure.Repository
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
