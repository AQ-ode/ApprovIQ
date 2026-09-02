using ApprovIQ.Domain.Entities;
using ApprovIQ.Domain.Enums;
using ApprovIQ.Domain.Interfaces;
using ApprovIQ.Infrastructure.Persistence;
using ApprovIQ.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace ApprovIQ.Application.Features.PurchaseOrders.Create;

public class CreatePOHandler : IRequestHandler<CreatePOCommand, CreatePOResponse>
{
    private readonly AppDbContext _db;
    private readonly ITenantContext _tenantContext;

    public CreatePOHandler(AppDbContext db, ITenantContext tenantContext)
    {
        _db = db;
        _tenantContext = tenantContext;

    }

    public async Task<CreatePOResponse> Handle(
        CreatePOCommand request,
        CancellationToken cancellationToken)
    {
        var poCount = await _db.PurchaseOrders
            .CountAsync(cancellationToken);
       
        var poNumber = $"PO-{DateTime.UtcNow.Year}-{(poCount + 1):D4}";

        var po = new PurchaseOrder
        {
            PONumber = poNumber,
            Title = request.Title,
            Description = request.Description,
            Status = POStatus.Draft,
            VendorId = request.VendorId,
            BudgetId = request.BudgetId,
            RequiredByDate = request.RequiredByDate,
            TenantId = _tenantContext.TenantId,
            RequestedById = Guid.Parse("55555555-5555-5555-5555-555555555555")
        };

        foreach (var item in request.LineItems)
        {
            po.LineItems.Add(new POLineItem
            {
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Unit = item.Unit
            });
        }

        po.TotalAmount = po.LineItems.Sum(x => x.Quantity * x.UnitPrice);

        _db.PurchaseOrders.Add(po);
        await _db.SaveChangesAsync(cancellationToken);

        return new CreatePOResponse(
            po.Id,
            po.PONumber,
            po.Title,
            po.Status.ToString()
        );
    }
}