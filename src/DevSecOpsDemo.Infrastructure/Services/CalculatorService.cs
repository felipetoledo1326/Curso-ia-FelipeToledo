using DevSecOpsDemo.Domain.Interfaces;

namespace DevSecOpsDemo.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de calculadora
/// </summary>
public class CalculatorService : ICalculatorService
{
    /// <summary>
    /// Suma dos números enteros
    /// </summary>
    /// <param name="a">Primer número</param>
    /// <param name="b">Segundo número</param>
    /// <returns>El resultado de la suma</returns>
    public int Sum(int a, int b)
    {
        return a + b;
    }
}
