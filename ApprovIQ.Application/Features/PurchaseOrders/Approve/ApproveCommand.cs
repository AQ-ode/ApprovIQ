using MediatR;

namespace ApprovIQ.Application.Features.PurchaseOrders.Approve;

public record ApprovePOCommand(
    Guid PurchaseOrderId
) : IRequest<ApprovePOResponse>;

public record ApprovePOResponse(
    Guid Id,
    string PONumber,
    string Status,
    DateTime ApprovedAt
);