using ApprovIQ.Application.Features.PurchaseOrders.Submit;
using MediatR;

namespace ApprovIQ.Api.Features.PurchaseOrders;

public static class SubmitEndpoint
{
    public static void MapSubmitPO(this WebApplication app)
    {
        app.MapPost("/api/purchase-orders/{id}/submit", async (
            Guid id,
            ISender sender) =>
        {
            var command = new SubmitPOCommand(id);
            var result = await sender.Send(command);
            return Results.Ok(result);
        })
        .WithName("SubmitPurchaseOrder")
        .WithTags("Purchase Orders")
        .Produces<SubmitPOResponse>(200)
        .WithSummary("Submit a purchase order for approval");
    }
}