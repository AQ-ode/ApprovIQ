using ApprovIQ.Domain.Interfaces;

namespace ApprovIQ.Infrastructure.Services;

public class TenantContext : ITenantContext
{
    public Guid TenantId { get; set; }
    public string TenantName { get; set; } = string.Empty;
}