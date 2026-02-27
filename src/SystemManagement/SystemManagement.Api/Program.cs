using AccessCardManagement.Infrastructure.Consumers;
using AccessCardManagement.Presentation;
using BuildingBlocks.Messaging.Extensions;
using LeaveManagement.Presentation;
using ReportManagement.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register modules
builder.Services.AddAccessCardModule(builder.Configuration);
builder.Services.AddLeaveModule(builder.Configuration);
builder.Services.AddReportModule(builder.Configuration);

// MassTransit + RabbitMQ (with consumers)
builder.Services.AddMessageBus(builder.Configuration, x =>
{
    x.AddConsumer<UserCreatedConsumer>();
});

var app = builder.Build();

// Initialize module databases
AccessCardModule.InitializeAccessCardDatabase(app.Services);
LeaveModule.InitializeLeaveDatabase(app.Services);
ReportModule.InitializeReportDatabase(app.Services);

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Service = "SystemManagement" }));

// Map module endpoints
app.MapAccessCardEndpoints();
app.MapLeaveEndpoints();
app.MapReportEndpoints();

app.Run();
