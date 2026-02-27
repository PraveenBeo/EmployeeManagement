using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LeaveManagement.Application.Interfaces;
using LeaveManagement.Domain.Entities;
using LeaveManagement.Infrastructure.Data;
using LeaveManagement.Infrastructure.Repositories;

namespace LeaveManagement.Presentation;

public static class LeaveModule
{
    public static IServiceCollection AddLeaveModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LeaveDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("LeaveConnection")));
        services.AddScoped<ILeaveRepository, LeaveRepository>();
        return services;
    }

    public static IEndpointRouteBuilder MapLeaveEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/leaves", async (ILeaveRepository repo) =>
            Results.Ok(await repo.GetAllAsync()));

        endpoints.MapPost("/leaves", async (LeaveRequest leave, ILeaveRepository repo) =>
            Results.Created($"/leaves/{leave.Id}", await repo.AddAsync(leave)));

        return endpoints;
    }

    public static void InitializeLeaveDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LeaveDbContext>();
        db.Database.EnsureCreated();
    }
}
