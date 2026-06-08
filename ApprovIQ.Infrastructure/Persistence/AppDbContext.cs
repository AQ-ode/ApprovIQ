using ApprovIQ.Domain.Common;
using ApprovIQ.Domain.Entities;
using ApprovIQ.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace ApprovIQ.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly Guid _tenantId;

    public AppDbContext(DbContextOptions<AppDbContext> options,
                        ITenantContext tenantContext) : base(options)
    {
        _tenantId = tenantContext.TenantId;
    }

    // Your tables
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<POLineItem> POLineItems => Set<POLineItem>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Budget> Budgets => Set<Budget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Global Query Filter - Multi-tenancy magic
        modelBuilder.Entity<PurchaseOrder>()
            .HasQueryFilter(x => x.TenantId == _tenantId && !x.IsDeleted);

        modelBuilder.Entity<Vendor>()
            .HasQueryFilter(x => x.TenantId == _tenantId && !x.IsDeleted);

        modelBuilder.Entity<Budget>()
            .HasQueryFilter(x => x.TenantId == _tenantId && !x.IsDeleted);

        modelBuilder.Entity<POLineItem>()
            .HasQueryFilter(x => !x.IsDeleted);

        // Decimal precision for money fields
        modelBuilder.Entity<PurchaseOrder>()
            .Property(x => x.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<POLineItem>()
            .Property(x => x.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Budget>()
            .Property(x => x.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Budget>()
            .Property(x => x.SpentAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Budget>()
    .Property(x => x.AlertThresholdPercent)
    .HasPrecision(18, 2);
    }

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        // Auto set UpdatedAt on every save
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}