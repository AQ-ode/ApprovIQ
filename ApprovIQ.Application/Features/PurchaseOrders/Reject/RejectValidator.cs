using FluentValidation;

namespace ApprovIQ.Application.Features.PurchaseOrders.Reject;

public class RejectPOValidator : AbstractValidator<RejectPOCommand>
{
    public RejectPOValidator()
    {
        RuleFor(x => x.PurchaseOrderId)
            .NotEmpty().WithMessage("Purchase Order ID is required");

        RuleFor(x => x.RejectionReason)
            .NotEmpty().WithMessage("Rejection reason is required")
            .MaximumLength(500).WithMessage("Rejection reason cannot exceed 500 characters");
    }
}