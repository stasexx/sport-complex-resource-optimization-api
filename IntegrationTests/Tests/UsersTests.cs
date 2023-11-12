using System.Net;
using System.Net.Http.Json;
using SmartInventorySystemApi.IntegrationTests;
using SmartInventorySystemApi.IntegrationTests.Tests;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
using Xunit;

namespace IntegrationTests.Tests;

public class UsersTests : TestsBase
{
    public UsersTests(TestingFactory<Program> factory) : base(factory, "users"){ }
    
    [Fact]
    public async Task RegisterAsync_ValidInput_ReturnsTokens()
    {
        // Arrange
        var register = new UserCreateDto()
        {
            Email = "register@gmail.com",
            Password = "Yuiop12345",
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{ResourceUrl}/register", register);
        var tokens = await response.Content.ReadFromJsonAsync<TokensModel>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(tokens);
        Assert.NotNull(tokens.AccessToken);
        Assert.NotNull(tokens.RefreshToken);
    }
}