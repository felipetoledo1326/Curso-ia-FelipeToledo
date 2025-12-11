using DevSecOpsDemo.Domain.Interfaces;

namespace DevSecOpsDemo.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de verificación de salud
/// </summary>
public class HealthService : IHealthService
{
    /// <summary>
    /// Obtiene el estado de salud del servicio
    /// </summary>
    /// <returns>Estado de salud del servicio</returns>
    public string GetHealthStatus()
    {
        return "ok";
    }
}
