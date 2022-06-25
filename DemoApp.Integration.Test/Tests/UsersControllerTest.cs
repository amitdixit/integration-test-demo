using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using UserApi.Models;

namespace DemoApp.Integration.Test.Tests;
public class UsersControllerTest : IntegrationTest
{
    [Fact]
    public async Task GetAllUser_WhenAuthenticated()
    {
        // Arrange
        await AuthenticateAsync();
        // Act

        var response = await HttpTestClient.GetAsync("https://localhost:7047/api/users/getUsers");
        // Assert 
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        (await response.Content.ReadFromJsonAsync<IEnumerable<User>>()).Should().NotBeNullOrEmpty();

    }

    [Fact]
    public async Task GetUser_WhenAuthenticatedAnd_UserId_IsProvided()
    {
        // Arrange
        await AuthenticateAsync();
        // Act
        int userId = 2;
        var response = await HttpTestClient.GetAsync($"https://localhost:7047/api/users/{userId}");
        // Assert 
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var user = await response.Content.ReadFromJsonAsync<User>();

        user.Should().NotBeNull();

        user.UserId.Should().Be(userId);
        user.UserName.Should().Be("sandhya.d");
    }

   
}

