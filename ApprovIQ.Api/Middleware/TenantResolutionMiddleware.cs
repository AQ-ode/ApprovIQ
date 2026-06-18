using ApprovIQ.Domain.Interfaces;
using ApprovIQ.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace ApprovIQ.Api.Middleware;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        AppDbContext db,
        ITenantContext tenantContext)
    {
        // Get subdomain from Host header
        var host = context.Request.Host.Host;
        var subdomain = host.Split('.')[0];

        // Hardcode for now - later read from subdomain
        if (subdomain == "localhost")
        {
            // Default tenant for testing
            tenantContext.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        }
        else
        {
            // Look up tenant by subdomain
            var tenant = await db.Tenants
                .FirstOrDefaultAsync(t => t.Subdomain == subdomain);

            if (tenant != null)
            {
                tenantContext.TenantId = tenant.Id;
            }
        }

        await _next(context);
    }
}