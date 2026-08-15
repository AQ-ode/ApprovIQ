using ApprovIQ.Application.Features.PurchaseOrders.Create;
using MediatR;

namespace ApprovIQ.Api.Features.PurchaseOrders;

public static class CreatePOEndpoint
{
    public static void MapCreatePO(this WebApplication app)
    {
        app.MapPost("/api/purchase-orders", async (
            CreatePOCommand command,
            ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created(
                $"/api/purchase-orders/{result.Id}", result);
        })
        .WithName("CreatePurchaseOrder")
        .WithTags("Purchase Orders")
        .Produces<CreatePOResponse>(201)
        .WithSummary("Create a new purchase order");
    }
}