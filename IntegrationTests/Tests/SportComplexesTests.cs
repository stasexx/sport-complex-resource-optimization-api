using System.Net;
using System.Net.Http.Json;
using Application.Models.Dtos;
using Application.Models.HideDto;
using SmartInventorySystemApi.IntegrationTests;
using SmartInventorySystemApi.IntegrationTests.Tests;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;
using Xunit;

namespace IntegrationTests.Tests;

public class SportComplexesTests : TestsBase
{
    public SportComplexesTests(TestingFactory<Program> factory) : base(factory, "sportcomplexes"){ }

    [Fact]
    public async Task CreateSportComplex_Valid_ByOwner()
    {
        // Arrange
        await LoginAsync("ownerAdrenaline@gmail.com", "adrenaline1234");
        var ownerId = "652c3b89ae02a3135d6519fc";
        var sportComplexCreateDto = new SportComplexCreateDto()
        {
            Name = "SuperGym",
            Email = "Some@gmail.com",
            City = "London",
            Address = "someswhere",
        };
        // Act
        
        var response = await HttpClient.PostAsJsonAsync($"{ResourceUrl}/create/{ownerId}", sportComplexCreateDto);
        var newSportComplex = await response.Content.ReadFromJsonAsync<SportComplexDto>();
        
        // Assert
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newSportComplex);
        Assert.Equal(sportComplexCreateDto.Email, newSportComplex.Email);
        Assert.Equal(sportComplexCreateDto.Address, newSportComplex.Address);
        Assert.Equal(sportComplexCreateDto.City, newSportComplex.City);
        Assert.Equal(sportComplexCreateDto.Name, newSportComplex.Name);
    }
    
    [Fact]
    public async Task CreateSportComplex_Invalid_ByUser()
    {
        // Arrange
        await LoginAsync("someone@gmail.com", "qwerty1234");
        var ownerId = "652c3b89ae02a3135d6519fc";
        var sportComplexCreateDto = new SportComplexCreateDto()
        {
            Name = "SuperGym",
            Email = "Some@gmail.com",
            City = "London",
            Address = "someswhere",
        };
        // Act
        
        var response = await HttpClient.PostAsJsonAsync($"{ResourceUrl}/create/{ownerId}", sportComplexCreateDto);
        
        // Assert
        
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateSportComplex_Valid_ByOwner()
    {
        // Arrange
        await LoginAsync("ownerAdrenaline@gmail.com", "adrenaline1234");
        var ownerId = "652c3b89ae02a3135d6519fc";
        var sportComplexUpdateDto = new SportComplexUpdateDto()
        {
            Id = "652c3b89ae02a3135d5409ff",
            Name = "Sport Complex UPDATE 3",
            Email = "superGym123@gmail.com",
            City = "Chernihivka",
            Address = "Soborna312",
            Description = "New Description",
            Rating = 3.1
        };
        // Act
        
        var response = await HttpClient.PutAsJsonAsync($"{ResourceUrl}/update", sportComplexUpdateDto);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {content}");
        var newSportComplex = await response.Content.ReadFromJsonAsync<SportComplexDto>();
        
        // Assert
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newSportComplex);
        Assert.Equal(sportComplexUpdateDto.Email, newSportComplex.Email);
        Assert.Equal(sportComplexUpdateDto.Address, newSportComplex.Address);
        Assert.Equal(sportComplexUpdateDto.City, newSportComplex.City);
        Assert.Equal(sportComplexUpdateDto.Name, newSportComplex.Name);
    }
    
    [Fact]
    public async Task HideSportComplex_Valid_ByOwner()
    {
        // Arrange
        await LoginAsync("ownerAdrenaline@gmail.com", "adrenaline1234");
        var complexId = "652c3b89ae02a3135d5409ff";

        // Act
        
        var response = await HttpClient.PutAsJsonAsync($"{ResourceUrl}/hide/{complexId}", complexId);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {content}");
        var newSportComplex = await response.Content.ReadFromJsonAsync<SportComplexDto>();
        
        // Assert
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newSportComplex);
    }
    
    [Fact]
    public async Task RevealSportComplex_Valid_ByOwner()
    {
        // Arrange
        await LoginAsync("ownerAdrenaline@gmail.com", "adrenaline1234");
        var complexId = "652c3b89ae02a3135d5409ff";

        // Act
        
        var response = await HttpClient.PutAsJsonAsync($"{ResourceUrl}/reveal/{complexId}", complexId);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {content}");
        var newSportComplex = await response.Content.ReadFromJsonAsync<SportComplexDto>();
        
        // Assert
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newSportComplex);
    }
    
}