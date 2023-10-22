namespace EquationGenerator.Services
{
    public interface ISettingsService
    {
        int ComputigObectSteps { get; }
        SettingsDefinition Settings { get; set; }
    }
}