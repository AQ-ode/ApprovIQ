using ApprovIQ.Domain.Enums;
using ApprovIQ.Domain.Interfaces;
using ApprovIQ.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApprovIQ.Application.Features.PurchaseOrders.Submit;

public class SubmitPOHandler : IRequestHandler<SubmitPOCommand, SubmitPOResponse>
{
    private readonly AppDbContext _db;
    private readonly ITenantContext _tenantContext;

    public SubmitPOHandler(AppDbContext db, ITenantContext tenantContext)
    {
        _db = db;
        _tenantContext = tenantContext;
    }

    public async Task<SubmitPOResponse> Handle(
        SubmitPOCommand request,
        CancellationToken cancellationToken)
    {
        // Find the PO
        var po = await _db.PurchaseOrders
            .FirstOrDefaultAsync(x => x.Id == request.PurchaseOrderId, cancellationToken);

        if (po == null)
            throw new Exception("Purchase Order not found");

        if (po.Status != POStatus.Draft)
            throw new Exception("Only Draft POs can be submitted");

        // Submit it
        po.Status = POStatus.Submitted;

        await _db.SaveChangesAsync(cancellationToken);

        return new SubmitPOResponse(
            po.Id,
            po.PONumber,
            po.Status.ToString()
        );
    }
}