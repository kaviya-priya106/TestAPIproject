//using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeOrderManagementAPI.Domain
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string? Name { get; set; }

        public decimal Salary { get; private set; }


        public void UpdateSalary(decimal salary)
        {
            if (salary <= 0)
                throw new Exception("Salary must be greater than zero");

            Salary = salary;
        }

        public string? Department { get; set; }

        public string? Role { get; set; }

        public int? manager_id { get; set; }
    }

    /*public class CreateEmployeeValidator : AbstractValidator<LoginDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Price)
               .MinimumLength(4);
        }
    } */
}
