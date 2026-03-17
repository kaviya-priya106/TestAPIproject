using System.ComponentModel.DataAnnotations;

namespace TestAPIproject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Salary { get; set; }

        public string? Department { get; set; }

        public string? Role { get; set; }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
