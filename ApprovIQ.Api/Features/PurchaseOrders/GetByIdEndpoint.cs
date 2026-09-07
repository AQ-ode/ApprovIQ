using ApprovIQ.Application.Features.PurchaseOrders.GetById;
using MediatR;

namespace ApprovIQ.Api.Features.PurchaseOrders;

public static class GetByIdEndpoint
{
    public static void MapGetPOById(this WebApplication app)
    {
        app.MapGet("/api/purchase-orders/{id}", async (
            Guid id,
            ISender sender) =>
        {
            var query = new GetByIdQuery(id);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetPurchaseOrderById")
        .WithTags("Purchase Orders")
        .Produces<GetByIdResponse>(200)
        .WithSummary("Get a single purchase order by ID");
    }
}