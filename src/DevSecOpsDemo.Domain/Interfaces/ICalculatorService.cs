namespace DevSecOpsDemo.Domain.Interfaces;

/// <summary>
/// Interfaz que define el contrato para operaciones de cálculo
/// </summary>
public interface ICalculatorService
{
    /// <summary>
    /// Suma dos números enteros
    /// </summary>
    /// <param name="a">Primer número</param>
    /// <param name="b">Segundo número</param>
    /// <returns>El resultado de la suma</returns>
    int Sum(int a, int b);
}
