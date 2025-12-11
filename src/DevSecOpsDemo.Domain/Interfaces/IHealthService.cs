namespace DevSecOpsDemo.Domain.Interfaces;

/// <summary>
/// Interfaz que define el contrato para verificación de salud del servicio
/// </summary>
public interface IHealthService
{
    /// <summary>
    /// Obtiene el estado de salud del servicio
    /// </summary>
    /// <returns>Estado de salud del servicio</returns>
    string GetHealthStatus();
}
