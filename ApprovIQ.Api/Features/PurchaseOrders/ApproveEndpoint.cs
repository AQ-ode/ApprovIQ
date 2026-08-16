using ApprovIQ.Application.Features.PurchaseOrders.Approve;
using MediatR;

namespace ApprovIQ.Api.Features.PurchaseOrders;

public static class ApproveEndpoint
{
    public static void MapApprovePO(this WebApplication app)
    {
        app.MapPost("/api/purchase-orders/{id}/approve", async (
            Guid id,
            ISender sender) =>
        {
            var command = new ApprovePOCommand(id);
            var result = await sender.Send(command);
            return Results.Ok(result);
        })
        .WithName("ApprovePurchaseOrder")
        .WithTags("Purchase Orders")
        .Produces<ApprovePOResponse>(200)
        .WithSummary("Approve a purchase order");
    }
}