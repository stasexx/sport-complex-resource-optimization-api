using System.Net;
using System.Net.Http.Json;
using Application.Models.Dtos;
using SmartInventorySystemApi.IntegrationTests;
using SmartInventorySystemApi.IntegrationTests.Tests;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
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
    
}