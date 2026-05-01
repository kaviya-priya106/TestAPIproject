using System.ComponentModel.DataAnnotations;

namespace TestAPIproject.ViewModels
{
       public class EmployeeDto
       {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Department { get; set; }
        }

        public class EmployeeCreateDto
        {


        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        public string Department { get; set; }

        public string Role { get; set; }
    }

        public class EmployeeEditDto
        {
        [Required]
            public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(10000, 1000000)]
        public int Salary { get; set; }
        }
  }

