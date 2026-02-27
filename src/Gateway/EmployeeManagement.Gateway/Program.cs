var builder = WebApplication.CreateBuilder(args);

// YARP Reverse Proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Gateway",
        Version = "v1"
    });
});

// HttpClients for health aggregation
builder.Services.AddHttpClient("UserManagementHealth", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["DownstreamServices:UserManagementApi"]!);
});
builder.Services.AddHttpClient("SystemManagementHealth", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["DownstreamServices:SystemManagementApi"]!);
});

var app = builder.Build();

// Swagger UI with downstream API docs
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");
    c.SwaggerEndpoint("/swagger/user/v1/swagger.json", "User Management API");
    c.SwaggerEndpoint("/swagger/system/v1/swagger.json", "System Management API");
});

// Health aggregation endpoint
app.MapGet("/health", async (IHttpClientFactory httpClientFactory) =>
{
    var gatewayHealth = new { Status = "Healthy", Service = "Gateway" };

    object? userHealth = null;
    object? systemHealth = null;

    try
    {
        var userClient = httpClientFactory.CreateClient("UserManagementHealth");
        var userResponse = await userClient.GetAsync("/health");
        if (userResponse.IsSuccessStatusCode)
            userHealth = await userResponse.Content.ReadFromJsonAsync<object>();
    }
    catch
    {
        userHealth = new { Status = "Unhealthy", Service = "UserManagement" };
    }

    try
    {
        var systemClient = httpClientFactory.CreateClient("SystemManagementHealth");
        var systemResponse = await systemClient.GetAsync("/health");
        if (systemResponse.IsSuccessStatusCode)
            systemHealth = await systemResponse.Content.ReadFromJsonAsync<object>();
    }
    catch
    {
        systemHealth = new { Status = "Unhealthy", Service = "SystemManagement" };
    }

    return Results.Ok(new
    {
        Gateway = gatewayHealth,
        Downstream = new { UserManagement = userHealth, SystemManagement = systemHealth }
    });
});

// YARP reverse proxy middleware (must be last)
app.MapReverseProxy();

app.Run();
