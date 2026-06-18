using FluentValidation;

namespace ApprovIQ.Application.Features.PurchaseOrders.Create;

public class CreatePOValidator : AbstractValidator<CreatePOCommand>
{
    public CreatePOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.VendorId)
            .NotEmpty().WithMessage("Vendor is required");

        RuleFor(x => x.BudgetId)
            .NotEmpty().WithMessage("Budget is required");

        RuleFor(x => x.LineItems)
            .NotEmpty().WithMessage("At least one line item is required");

        RuleForEach(x => x.LineItems).ChildRules(item =>
        {
            item.RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Line item description is required");

            item.RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            item.RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than 0");
        });
    }
}