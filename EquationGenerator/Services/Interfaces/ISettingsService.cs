namespace EquationGenerator.Services.Interfaces
{
    public interface ISettingsService
    {
        int ComputigObectSteps { get; }
        SettingsDefinition Settings { get; set; }
    }
}