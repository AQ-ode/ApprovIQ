using MediatR;

namespace ApprovIQ.Application.Features.PurchaseOrders.Submit;

public record SubmitPOCommand(
    Guid PurchaseOrderId
) : IRequest<SubmitPOResponse>;

public record SubmitPOResponse(
    Guid Id,
    string PONumber,
    string Status
);