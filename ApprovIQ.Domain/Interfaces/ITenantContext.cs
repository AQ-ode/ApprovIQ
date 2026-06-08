namespace ApprovIQ.Domain.Interfaces;

public interface ITenantContext
{
    Guid TenantId { get; set; }
    string TenantName { get; set; }
}