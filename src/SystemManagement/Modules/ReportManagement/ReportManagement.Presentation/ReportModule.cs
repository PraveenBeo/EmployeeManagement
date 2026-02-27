using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportManagement.Application.Interfaces;
using ReportManagement.Domain.Entities;
using ReportManagement.Infrastructure.Data;
using ReportManagement.Infrastructure.Repositories;

namespace ReportManagement.Presentation;

public static class ReportModule
{
    public static IServiceCollection AddReportModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReportDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ReportConnection")));
        services.AddScoped<IReportRepository, ReportRepository>();
        return services;
    }

    public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/reports", async (IReportRepository repo) =>
            Results.Ok(await repo.GetAllAsync()));

        endpoints.MapPost("/reports", async (Report report, IReportRepository repo) =>
            Results.Created($"/reports/{report.Id}", await repo.AddAsync(report)));

        return endpoints;
    }

    public static void InitializeReportDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
        db.Database.EnsureCreated();
    }
}
