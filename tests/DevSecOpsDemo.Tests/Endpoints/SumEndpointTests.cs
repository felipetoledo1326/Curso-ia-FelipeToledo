using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DevSecOpsDemo.Application.DTOs;
using DevSecOpsDemo.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace DevSecOpsDemo.Tests.Endpoints;

/// <summary>
/// Pruebas de integración para el endpoint POST /api/suma
/// </summary>
public class SumEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SumEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    #region Casos Válidos

    [Fact]
    public async Task PostSum_WithValidNumbers_ReturnsOkStatusCode()
    {
        // Arrange
        var request = new SumRequest { A = 5, B = 3 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/suma", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostSum_WithValidNumbers_ReturnsCorrectResult()
    {
        // Arrange
        var request = new SumRequest { A = 10, B = 25 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/suma", request);
        var content = await response.Content.ReadAsStringAsync();
        var sumResponse = JsonSerializer.Deserialize<SumResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        sumResponse.Should().NotBeNull();
        sumResponse!.Result.Should().Be(35);
    }

    [Fact]
    public async Task PostSum_WithNegativeNumbers_ReturnsCorrectResult()
    {
        // Arrange
        var request = new SumRequest { A = -5, B = 3 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/suma", request);
        var content = await response.Content.ReadAsStringAsync();
        var sumResponse = JsonSerializer.Deserialize<SumResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        sumResponse.Should().NotBeNull();
        sumResponse!.Result.Should().Be(-2);
    }

    [Fact]
    public async Task PostSum_WithZeroValues_ReturnsCorrectResult()
    {
        // Arrange
        var request = new SumRequest { A = 0, B = 0 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/suma", request);
        var content = await response.Content.ReadAsStringAsync();
        var sumResponse = JsonSerializer.Deserialize<SumResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        sumResponse.Should().NotBeNull();
        sumResponse!.Result.Should().Be(0);
    }

    [Fact]
    public async Task PostSum_WithLargeNumbers_ReturnsCorrectResult()
    {
        // Arrange
        var request = new SumRequest { A = 1000000, B = 2000000 };

        // Act
        var response = await _client.PostAsJsonAsync("/api/suma", request);
        var content = await response.Content.ReadAsStringAsync();
        var sumResponse = JsonSerializer.Deserialize<SumResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        sumResponse.Should().NotBeNull();
        sumResponse!.Result.Should().Be(3000000);
    }

    #endregion

    #region Casos Inválidos

    [Fact]
    public async Task PostSum_WithNullBody_ReturnsBadRequest()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/suma", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostSum_WithNullBody_ReturnsErrorMessage()
    {
        // Arrange
        var content = new StringContent("", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/suma", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseContent.Should().Contain("error");
        responseContent.Should().Contain("body");
    }

    [Fact]
    public async Task PostSum_WithEmptyJson_ReturnsOkWithZeroResult()
    {
        // Arrange
        var content = new StringContent("{}", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/suma", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var sumResponse = JsonSerializer.Deserialize<SumResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        sumResponse.Should().NotBeNull();
        sumResponse!.Result.Should().Be(0); // Valores por defecto de int son 0
    }

    #endregion
}
