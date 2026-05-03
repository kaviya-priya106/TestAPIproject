using FluentValidation;
using EmployeeOrderManagementAPI.Application.Dto;

namespace EmployeeOrderManagementAPI.Application.Validators
{
    public class EmployeeValidator: AbstractValidator<EmployeeDto>
    {

    }

    public class PatchEmployeeDtoValidator : AbstractValidator<PatchEmployeeDto>
    {
        public PatchEmployeeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .When(x => x.Name != null);

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .When(x => x.Salary.HasValue);

            RuleFor(x => x.Department)
    .NotEmpty().WithMessage("Department cannot be empty")
    .MinimumLength(2)
    .MaximumLength(50)
    .When(x => x.Department != null);

            RuleFor(x => x.Role)
    .NotEmpty().WithMessage("Role cannot be empty")
    .MinimumLength(2)
    .MaximumLength(50)
    .When(x => x.Role != null);

            RuleFor(x => x.Manager_Id)
    .GreaterThan(0).WithMessage("Manager_Id must be greater than 0")
    .When(x => x.Manager_Id.HasValue);
        }
    }
}
