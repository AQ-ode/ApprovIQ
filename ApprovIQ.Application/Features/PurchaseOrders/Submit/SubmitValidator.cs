using FluentValidation;

namespace ApprovIQ.Application.Features.PurchaseOrders.Submit;

public class SubmitPOValidator : AbstractValidator<SubmitPOCommand>
{
    public SubmitPOValidator()
    {
        RuleFor(x => x.PurchaseOrderId)
            .NotEmpty().WithMessage("Purchase Order ID is required");
    }
}