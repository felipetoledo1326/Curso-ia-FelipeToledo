using Microsoft.AspNetCore.Mvc.Testing;

namespace DevSecOpsDemo.Tests.Infrastructure;

/// <summary>
/// Factory personalizada para crear la aplicación web en memoria para pruebas de integración
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    // Por ahora no necesitamos configuración adicional
    // Esta clase puede extenderse en el futuro para sobrescribir servicios o configuración
}
