using ApprovIQ.Api.Features.PurchaseOrders;
using ApprovIQ.Api.Middleware;
using ApprovIQ.Application.Features.PurchaseOrders.Create;
using ApprovIQ.Domain.Interfaces;
using ApprovIQ.Infrastructure.Persistence;
using ApprovIQ.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Multi-tenancy
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantContext, TenantContext>();
//checks
// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(CreatePOCommand).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatePOCommand>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<TenantResolutionMiddleware>();
app.UseHttpsRedirection();

// Map endpoints
app.MapCreatePO();
app.MapApprovePO();
app.MapRejectPO();
app.MapSubmitPO();
app.MapGetPOById();

app.MapGet("/", () => "ApprovIQ API is running!");

app.Run();