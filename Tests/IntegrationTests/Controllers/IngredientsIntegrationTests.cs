using System.Net;
using System.Net.Http.Json;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace DishCraft.Tests.IntegrationTests.Controllers;

[TestFixture]
public class IngredientsIntegrationTests : IDisposable
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private DataContext _context;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the app's DataContext registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add database for testing
                    services.AddDbContext<DataContext>(options =>
                    {
                        options.UseInMemoryDatabase("IntegrationTestsDb");
                    });
                });
            });

        _client = _factory.CreateClient();
    }

    [Test]
    public async Task CreateIngredient_EndToEnd_Success()
    {
        // Arrange
        var ingredient = new IngredientDto
        {
            Name = "Integration Test Ingredient",
            Category = "Spice",
            MeasuredIn = "Weight",
            WeightUnit = "Grams",
            PricePerPackage = 5.99m,
            Quantity = 500
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/ingredients", ingredient);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        var createdIngredient = await response.Content.ReadFromJsonAsync<IngredientDto>();
        Assert.That(createdIngredient.Name, Is.EqualTo("Integration Test Ingredient"));
    }

    public void Dispose()
    {
        _factory?.Dispose();
        _client?.Dispose();
    }
}