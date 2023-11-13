using System.Net;
using System.Net.Http.Json;
using Amazon.Runtime.Internal;
using MongoDB.Bson;
using SmartInventorySystemApi.IntegrationTests;
using SmartInventorySystemApi.IntegrationTests.Tests;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
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
            Phone = "+38066656564",
            Password = "qwerty12324",
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
    
    [Fact]
    public async Task RegisterAsync_ExistingEmail_Returns409Conflict()
    {
        // Arrange
        var register = new UserCreateDto
        {
            Email = "someone@gmail.com",
            Phone = "+380666565643",
            Password = "Yuiop12345",
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{ResourceUrl}/register", register);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
    
    [Fact]
    public async Task LoginAsync_ValidInput_ReturnsTokens()
    {
        // Arrange
        var login = new LoginUserDto()
        {
            Email = "someone@gmail.com",
            Password = "qwerty1234"
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{ResourceUrl}/login", login);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {content}");
        var tokens = await response.Content.ReadFromJsonAsync<TokensModel>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(tokens);
        Assert.NotNull(tokens.AccessToken);
        Assert.NotNull(tokens.RefreshToken);
    }
    
    [Fact]
    public async Task LoginAsync_InvalidInput_ReturnsTokens()
    {
        // Arrange
        var login = new LoginUserDto()
        {
            Email = "undefinde@gmail.com",
            Password = "fsdfdsfdsg"
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{ResourceUrl}/login", login);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateAsync_ValidRequest_ReturnsUpdatedUser()
    {
        // Arrange
        await LoginAsync("someoneForUpdate@gmail.com", "update12345");
        var userUpdateDto = new UserUpdateDto()
        {
            Id = "652c3b89ae02a3135d6309fc",
            Email = "stanislav@gmail.com",
            Phone = "+3345343524324",
            FirstName = "Stanislav",
            LastName = "Kovac"
        };

        // Act
        var response = await HttpClient.PutAsJsonAsync($"{ResourceUrl}", userUpdateDto);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {content}");
        var updateUserModel = await response.Content.ReadFromJsonAsync<UpdateUserModel>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(updateUserModel);
        Assert.NotNull(updateUserModel.User);
        Assert.NotNull(updateUserModel.Tokens);
        Assert.NotNull(updateUserModel.Tokens.AccessToken);
        Assert.NotNull(updateUserModel.Tokens.RefreshToken);
        Assert.Equal(userUpdateDto.Email, updateUserModel.User.Email);
        Assert.Equal(userUpdateDto.FirstName, updateUserModel.User.FirstName);
        Assert.Equal(userUpdateDto.LastName, updateUserModel.User.LastName);
        Assert.Equal(userUpdateDto.Phone, updateUserModel.User.Phone);
    }

    [Fact]
    public async Task UpdateAsync_Unauthorized_Returns401Unauthorized()
    {
        // Arrange
        var userUpdateDto = new UserUpdateDto
        {
            Email = "newEmail@gmail.com"
        };

        // Act
        var response = await HttpClient.PutAsJsonAsync($"{ResourceUrl}", userUpdateDto);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}