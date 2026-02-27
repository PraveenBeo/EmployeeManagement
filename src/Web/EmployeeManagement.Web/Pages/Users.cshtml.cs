using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Web.Pages;

public class UsersModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UsersModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<UserDto>? Users { get; set; }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("UserApi");
        try
        {
            var response = await client.GetAsync("/users");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Users = JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        catch
        {
            Users = null;
        }
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedUtc { get; set; }
    }
}
