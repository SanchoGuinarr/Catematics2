namespace EquationGenerator.Services.Interfaces
{
    public interface IGeneratorService
    {
        AEquation GenerateIntEquation(ComplexityState state, bool random = true);
    }
}