using ApprovIQ.Application.Features.PurchaseOrders.Reject;
using MediatR;

namespace ApprovIQ.Api.Features.PurchaseOrders;

public static class RejectEndpoint
{
    public static void MapRejectPO(this WebApplication app)
    {
        app.MapPost("/api/purchase-orders/{id}/reject", async (
            Guid id,
            RejectPORequest request,
            ISender sender) =>
        {
            var command = new RejectPOCommand(id, request.RejectionReason);
            var result = await sender.Send(command);
            return Results.Ok(result);
        })
        .WithName("RejectPurchaseOrder")
        .WithTags("Purchase Orders")
        .Produces<RejectPOResponse>(200)
        .WithSummary("Reject a purchase order");
    }
}

public record RejectPORequest(string RejectionReason);