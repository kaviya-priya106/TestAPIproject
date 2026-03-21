using TestAPIproject.Models;
using TestAPIproject.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestAPIproject.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _repo;

        public AuthService(IConfiguration config, IUserRepository repo)
        {
            _config = config;
            _repo = repo;
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

        private string GenerateToken(Users user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );
            Console.WriteLine("GEN KEY: " + _config["Jwt:Key"]);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
