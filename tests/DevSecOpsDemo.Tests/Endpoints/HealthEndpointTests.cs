using System.Net;
using System.Text.Json;
using DevSecOpsDemo.Application.DTOs;
using DevSecOpsDemo.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace DevSecOpsDemo.Tests.Endpoints;

/// <summary>
/// Pruebas de integración para el endpoint GET /api/health
/// </summary>
public class HealthEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHealth_ReturnsOkStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/api/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetHealth_ReturnsCorrectStatusInBody()
    {
        // Act
        var response = await _client.GetAsync("/api/health");
        var content = await response.Content.ReadAsStringAsync();
        var healthResponse = JsonSerializer.Deserialize<HealthResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        healthResponse.Should().NotBeNull();
        healthResponse!.Status.Should().Be("ok");
        healthResponse.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    [Fact]
    public async Task GetHealth_ReturnsCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/health");

        // Assert
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.MediaType.Should().Be("application/json");
    }
}
