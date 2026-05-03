using FluentValidation;
using EmployeeOrderManagementAPI.Application.Dto;

public class PatchOrderDtoValidator : AbstractValidator<PatchOrderDto>
{
    public PatchOrderDtoValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name cannot be empty")
            .MinimumLength(3).WithMessage("Product name must be at least 3 characters")
            .MaximumLength(100)
            .When(x => x.ProductName != null);

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0")
            .When(x => x.Quantity.HasValue);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .When(x => x.Price.HasValue);
    }
}