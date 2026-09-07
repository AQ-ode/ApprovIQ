using ApprovIQ.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApprovIQ.Application.Features.PurchaseOrders.GetById;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, GetByIdResponse>
{
    private readonly AppDbContext _db;
    //cicd
    public GetByIdHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<GetByIdResponse> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        var po = await _db.PurchaseOrders
            .Include(x => x.LineItems)
            .FirstOrDefaultAsync(x => x.Id == request.PurchaseOrderId, cancellationToken);

        if (po == null)
            throw new Exception("Purchase Order not found");

        return new GetByIdResponse(
            po.Id,
            po.PONumber,
            po.Title,
            po.Description,
            po.Status.ToString(),
            po.TotalAmount,
            po.Currency,
            po.RequiredByDate,
            po.CreatedAt,
            po.ApprovedAt,
            po.RejectionReason,
            po.LineItems.Count
        );
    }
}