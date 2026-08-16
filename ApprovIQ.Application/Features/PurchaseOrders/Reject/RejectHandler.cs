using ApprovIQ.Domain.Enums;
using ApprovIQ.Domain.Interfaces;
using ApprovIQ.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApprovIQ.Application.Features.PurchaseOrders.Reject;

public class RejectPOHandler : IRequestHandler<RejectPOCommand, RejectPOResponse>
{
    private readonly AppDbContext _db;
    private readonly ITenantContext _tenantContext;

    public RejectPOHandler(AppDbContext db, ITenantContext tenantContext)
    {
        _db = db;
        _tenantContext = tenantContext;
    }

    public async Task<RejectPOResponse> Handle(
        RejectPOCommand request,
        CancellationToken cancellationToken)
    {
        // Find the PO
        var po = await _db.PurchaseOrders
            .FirstOrDefaultAsync(x => x.Id == request.PurchaseOrderId, cancellationToken);

        if (po == null)
            throw new Exception("Purchase Order not found");

        if (po.Status != POStatus.Draft && po.Status != POStatus.Submitted)
            throw new Exception("Only Draft or Submitted POs can be rejected");

        // Reject it
        po.Status = POStatus.Rejected;
        po.RejectionReason = request.RejectionReason;

        await _db.SaveChangesAsync(cancellationToken);

        return new RejectPOResponse(
            po.Id,
            po.PONumber,
            po.Status.ToString(),
            po.RejectionReason
        );
    }
}