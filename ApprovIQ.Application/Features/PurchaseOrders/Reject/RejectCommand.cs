using MediatR;

namespace ApprovIQ.Application.Features.PurchaseOrders.Reject;

public record RejectPOCommand(
    Guid PurchaseOrderId,
    string RejectionReason
) : IRequest<RejectPOResponse>;

public record RejectPOResponse(
    Guid Id,
    string PONumber,
    string Status,
    string RejectionReason
);