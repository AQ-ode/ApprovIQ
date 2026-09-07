using MediatR;

namespace ApprovIQ.Application.Features.PurchaseOrders.GetById;

public record GetByIdQuery(
    Guid PurchaseOrderId
) : IRequest<GetByIdResponse>;

public record GetByIdResponse(
    Guid Id,
    string PONumber,
    string Title,
    string Description,
    string Status,
    decimal TotalAmount,
    string Currency,
    DateTime? RequiredByDate,
    DateTime CreatedAt,
    DateTime? ApprovedAt,
    string? RejectionReason,
    int LineItemCount
);