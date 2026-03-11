using System.ComponentModel.DataAnnotations;

namespace TestAPIproject.ViewModels
{
       public class EmployeeListViewModel
       {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Department { get; set; }
        }

        public class EmployeeCreateViewModel
        {
       
       
            [Required]
            public string Name { get; set; }

        public string Department { get; set; }

        public string Role { get; set; }
    }

        public class EmployeeEditViewModel
        {
        [Required]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }
            public int Salary { get; set; }
        }
  }

