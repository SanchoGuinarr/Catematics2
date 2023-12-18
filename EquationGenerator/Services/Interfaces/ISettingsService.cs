namespace EquationGenerator.Services.Interfaces
{
    public interface ISettingsService
    {
        /// <summary>
        /// Steps in which computing objects will be generated 
        /// (i.e there is 10 computing objects and 66 steps, so computing objects will be generated in steps 6, 12, 18, 24, 30, 36, 42, 48, 54, 60)
        /// </summary>
        int ComputigObectSteps { get; }
        SettingsDefinition Settings { get; set; }
    }
}