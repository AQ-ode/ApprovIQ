using FluentValidation;

namespace ApprovIQ.Application.Features.PurchaseOrders.GetById;

public class GetByIdValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdValidator()
    {
        RuleFor(x => x.PurchaseOrderId)
            .NotEmpty().WithMessage("Purchase Order ID is required");
    }
}