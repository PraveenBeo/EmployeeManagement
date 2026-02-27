var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var gatewayBaseUrl = builder.Configuration["ServiceUrls:GatewayApi"]!;

builder.Services.AddHttpClient("UserApi", client =>
{
    client.BaseAddress = new Uri(gatewayBaseUrl);
});

builder.Services.AddHttpClient("SystemApi", client =>
{
    client.BaseAddress = new Uri(gatewayBaseUrl);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
