using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagement.Web.Pages;

public class SystemModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SystemModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<AccessCardDto>? AccessCards { get; set; }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("SystemApi");
        try
        {
            var response = await client.GetAsync("/api/system/accesscards");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                AccessCards = JsonSerializer.Deserialize<List<AccessCardDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        catch
        {
            AccessCards = null;
        }
    }

    public class AccessCardDto
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime CreatedUtc { get; set; }
    }
}
