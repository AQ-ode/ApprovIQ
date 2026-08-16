using ApprovIQ.Domain.Enums;
using ApprovIQ.Domain.Interfaces;
using ApprovIQ.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ApprovIQ.Application.Features.PurchaseOrders.Approve;

public class ApprovePOHandler : IRequestHandler<ApprovePOCommand, ApprovePOResponse>
{
    private readonly AppDbContext _db;
    private readonly ITenantContext _tenantContext;

    public ApprovePOHandler(AppDbContext db, ITenantContext tenantContext)
    {
        _db = db;
        _tenantContext = tenantContext;
    }

    public async Task<ApprovePOResponse> Handle(
        ApprovePOCommand request,
        CancellationToken cancellationToken)
    {
        // Find the PO
        var po = await _db.PurchaseOrders
            .FirstOrDefaultAsync(x => x.Id == request.PurchaseOrderId, cancellationToken);

        if (po == null)
            throw new Exception("Purchase Order not found");

        if (po.Status != POStatus.Draft && po.Status != POStatus.Submitted)
            throw new Exception("Only Draft or Submitted POs can be approved");

        // Approve it
        po.Status = POStatus.Approved;
        po.ApprovedAt = DateTime.UtcNow;
        po.ApprovedById = Guid.Parse("55555555-5555-5555-5555-555555555555"); // Will come from auth later

        await _db.SaveChangesAsync(cancellationToken);

        return new ApprovePOResponse(
            po.Id,
            po.PONumber,
            po.Status.ToString(),
            po.ApprovedAt.Value
        );
    }
}