namespace DevSecOpsDemo.Application.DTOs;

/// <summary>
/// DTO para la respuesta de health check
/// </summary>
public class HealthResponse
{
    /// <summary>
    /// Estado del servicio
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Marca de tiempo de la verificación
    /// </summary>
    public DateTime Timestamp { get; set; }
}
