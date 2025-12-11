namespace DevSecOpsDemo.Application.DTOs;

/// <summary>
/// DTO para la solicitud de suma
/// </summary>
public class SumRequest
{
    /// <summary>
    /// Primer número a sumar
    /// </summary>
    public int A { get; set; }

    /// <summary>
    /// Segundo número a sumar
    /// </summary>
    public int B { get; set; }
}
