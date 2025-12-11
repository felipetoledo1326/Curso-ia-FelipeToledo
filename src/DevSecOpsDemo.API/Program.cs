using DevSecOpsDemo.Application.DTOs;
using DevSecOpsDemo.Domain.Interfaces;
using DevSecOpsDemo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar servicios de dominio
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddScoped<IHealthService, HealthService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint GET /api/health
app.MapGet("/api/health", (IHealthService healthService) =>
{
    var response = new HealthResponse
    {
        Status = healthService.GetHealthStatus(),
        Timestamp = DateTime.UtcNow
    };
    
    return Results.Ok(response);
})
.WithName("GetHealth")
.WithOpenApi()
.Produces<HealthResponse>(StatusCodes.Status200OK);

// Endpoint POST /api/suma
app.MapPost("/api/suma", (SumRequest? request, ICalculatorService calculatorService) =>
{
    // Validar si el request es nulo
    if (request == null)
    {
        return Results.BadRequest(new 
        { 
            error = "El body de la solicitud no puede ser nulo",
            message = "Debe proporcionar un JSON válido con los campos A y B"
        });
    }

    // Realizar la suma
    var result = calculatorService.Sum(request.A, request.B);
    
    var response = new SumResponse
    {
        Result = result
    };
    
    return Results.Ok(response);
})
.WithName("PostSum")
.WithOpenApi()
.Produces<SumResponse>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

app.Run();

// Hacer la clase Program accesible para las pruebas de integración
public partial class Program { }

