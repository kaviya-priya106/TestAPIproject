using TestAPIproject.Models;
using TestAPIproject.Repository;

namespace TestAPIproject.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _repo = userRepository;
            _config = config;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _repo.GetUserByUsernameAsync(username);

            if (user == null)
                throw new Exception("User not found");

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid)
                throw new Exception("Invalid password");

            return GenerateToken(user);
        }
    }
}
