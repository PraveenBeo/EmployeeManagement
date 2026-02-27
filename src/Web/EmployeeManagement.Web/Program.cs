var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient("UserApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:UserManagementApi"]!);
});

builder.Services.AddHttpClient("SystemApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:SystemManagementApi"]!);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
