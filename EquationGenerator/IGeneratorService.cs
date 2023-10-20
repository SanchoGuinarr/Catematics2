namespace EquationGenerator
{
    public interface IGeneratorService
    {
        AEquation GenerateIntEquation(State state, bool random = true);
    }
}