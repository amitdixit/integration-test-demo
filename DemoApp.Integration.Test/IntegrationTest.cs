using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text;
using UserApi.Controllers;

namespace DemoApp.Integration.Test;
public class IntegrationTest
{
    protected readonly HttpClient HttpTestClient;

    protected IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Program>();

        HttpTestClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        HttpTestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
    }

    private async Task<string> GetJwtAsync()
    {
        var content = System.Text.Json.JsonSerializer.Serialize(new AuthRequest { UserName = "amit.d", Password = "Password1" });
        var data = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await HttpTestClient.PostAsync("https://localhost:7047/api/auth/validate", data);
        return await response.Content.ReadAsStringAsync();
    }
}
