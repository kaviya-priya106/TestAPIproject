using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeOrderManagementAPI.Infrastructure.Repository;
using EmployeeOrderManagementAPI.Infrastructure.Data;
using EmployeeOrderManagementAPI.Application.Interfaces;
using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _repo;
        private AppDbContext _context;

        public AuthService(IConfiguration config, IUserRepository repo, AppDbContext context)
        {
            _config = config;
            _repo = repo;
            _context = context;
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

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new Users
            {
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        private string GenerateToken(Users user)
        {
            var claims = new[]
            {
         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
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
