using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsDefinition Settings { get; set; }
        public int ComputigObectSteps { get; private set; }
        public SettingsService()
        {
            Settings = new()
            {
                ComputingObjectsCount = 10,
                InitialState = new()
                {
                    MaxNumAdd = 0,
                    MaxNumMulti = 0,
                    OperationCondition = new() { OperationCondition.addition },
                    ComplexityConditionAdd = ComplexityConditionAdd.none,
                    ComplexityConditionMulti = ComplexityConditionMulti.none
                },
                FinalState = new()
                {
                    MaxNumAdd = 40,
                    MaxNumMulti = 20,
                    OperationCondition = new() { OperationCondition.addition, OperationCondition.subtraction, OperationCondition.multiplication, OperationCondition.division },
                    ComplexityConditionAdd = ComplexityConditionAdd.overMultiTens,
                    ComplexityConditionMulti = ComplexityConditionMulti.overTwenty
                },
            };
            DetermineComputingObjectSteps(Settings);
        }

        private void DetermineComputingObjectSteps(SettingsDefinition settings)
        {
            // all computing objects should appears regulary during the process
            // example
            // start with 10 and finish with 50 add numbers
            // start with  5 and finish with 26 multi numbers
            // this is ((50 + 31) - (10 + 5)) = 66 number steps
            // 10 computing objects (adding one, because we dont start at 0)
            // (66 / 10 + 1) = 6
            // 6 -> 1. object
            // 12 -> 2. object
            // 18 -> 3. object
            // 24 -> 4. object
            // 30 -> 5. object
            // 36 -> 6. object
            // 42 -> 7. object
            // 48 -> 8. object
            // 54 -> 9. object
            // 60 -> 10. object
            ComputigObectSteps = (((settings.FinalState.MaxNumAdd + settings.FinalState.MaxNumMulti) - (settings.InitialState.MaxNumAdd + settings.InitialState.MaxNumMulti)) / (settings.ComputingObjectsCount + 1));
        }
    }
}
