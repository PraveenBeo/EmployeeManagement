using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AccessCardManagement.Application.Interfaces;
using AccessCardManagement.Domain.Entities;
using AccessCardManagement.Infrastructure.Data;
using AccessCardManagement.Infrastructure.Repositories;

namespace AccessCardManagement.Presentation;

public static class AccessCardModule
{
    public static IServiceCollection AddAccessCardModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AccessCardDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("AccessCardConnection")));
        services.AddScoped<IAccessCardRepository, AccessCardRepository>();
        return services;
    }

    public static IEndpointRouteBuilder MapAccessCardEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/accesscards", async (IAccessCardRepository repo) =>
            Results.Ok(await repo.GetAllAsync()));

        endpoints.MapPost("/accesscards", async (AccessCard card, IAccessCardRepository repo) =>
            Results.Created($"/accesscards/{card.Id}", await repo.AddAsync(card)));

        return endpoints;
    }

    public static void InitializeAccessCardDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AccessCardDbContext>();
        db.Database.EnsureCreated();
    }
}
