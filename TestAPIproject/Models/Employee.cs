using System.ComponentModel.DataAnnotations;

namespace TestAPIproject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Range(10000, 1000000)]
        public int Salary { get; set; }

        public string? Department { get; set; }

        public string? Role { get; set; }
    }
}
