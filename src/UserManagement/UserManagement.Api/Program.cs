using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Messaging.Extensions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Messaging;
using UserManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

// MassTransit + RabbitMQ (publisher only, no consumers)
builder.Services.AddMessageBus(builder.Configuration);
builder.Services.AddScoped<IEventPublisher, EventPublisher>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Service = "UserManagement" }));

app.MapGet("/users", async (IUserRepository repo) =>
    Results.Ok(await repo.GetAllAsync()));

app.MapPost("/users", async (User user, IUserRepository repo, IEventPublisher eventPublisher) =>
{
    var created = await repo.AddAsync(user);
    await eventPublisher.PublishUserCreatedAsync(created);
    return Results.Created($"/users/{created.Id}", created);
});

app.Run();
