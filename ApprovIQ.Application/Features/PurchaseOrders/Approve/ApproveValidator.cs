using FluentValidation;

namespace ApprovIQ.Application.Features.PurchaseOrders.Approve;

public class ApprovePOValidator : AbstractValidator<ApprovePOCommand>
{
    public ApprovePOValidator()
    {
        RuleFor(x => x.PurchaseOrderId)
            .NotEmpty().WithMessage("Purchase Order ID is required");
    }
}