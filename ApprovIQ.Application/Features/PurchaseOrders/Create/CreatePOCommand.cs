using MediatR;
using System;
using System.Collections.Generic;

namespace ApprovIQ.Application.Features.PurchaseOrders.Create;

public record CreatePOCommand(
    string Title,
    string Description,
    Guid VendorId,
    Guid BudgetId,
    DateTime? RequiredByDate,
    List<CreatePOLineItemDto> LineItems
) : IRequest<CreatePOResponse>;

public record CreatePOLineItemDto(
    string Description,
    int Quantity,
    decimal UnitPrice,
    string? Unit
);

public record CreatePOResponse(
    Guid Id,
    string PONumber,
    string Title,
    string Status
);