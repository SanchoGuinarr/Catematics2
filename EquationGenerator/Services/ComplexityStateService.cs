using EquationGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.Services
{
    public class ComplexityStateService : IComplexityStateService
    {
        private readonly IGeneratorService _generatorService;
        private readonly ISettingsService _settingsService;

        public MoneyCounter MoneyCounter { get; set; } = new() { Money = 10000 };

        public ComplexityStateService(IGeneratorService generatorService, ISettingsService settingsService)
        {
            _generatorService = generatorService;
            _settingsService = settingsService;
        }

        public bool CanGenerateNextComputingObject(ComplexityState state, int existedComputingOjects)
        {
            // number step - all numbers which should be generated (sum of maxNumAdd and maxNumMulti minus initial state)
            // example: there should be 66 number steps and computing object step is 6
            // supose there are 2 existing computing objects
            // current numbers: add 20, multi 10 
            // initial state: add 10, multi 5
            // current number step (20 +10) - (10 + 5) = 15
            // 15 / 6 = 2 - so result will be false
            // next computing object should be generated on current number step 6 * 3 = 18
            int currentNumberStep = GetCurrentNumberStep(state);
            bool result = currentNumberStep / _settingsService.ComputigObectSteps > existedComputingOjects;
            return result;
        }

        private int GetCurrentNumberStep(ComplexityState state)
        {
            return state.GetNumberStepValue() - _settingsService.Settings.InitialState.GetNumberStepValue();
        }

        public int GetCurrentStateComplexity(ComplexityState state)
        {
            return _generatorService.GenerateIntEquation(state, false).Complexity;
        }

        public bool IsComplexityConditionSatisfied(ComplexityConditionAdd complexityConditionAdd, ComplexityState state)
        {
            switch (complexityConditionAdd)
            {
                case ComplexityConditionAdd.none:
                    return true;
                case ComplexityConditionAdd.overTen:
                    return state.MaxNumAdd > 10;
                case ComplexityConditionAdd.overMultiTens:
                    return state.MaxNumAdd > 20;
                default:
                    throw new NotImplementedException("Unknown ComplexityCondition");
            }
        }

        public bool IsComplexityConditionSatisfied(ComplexityConditionMulti complexityConditionMulti, ComplexityState state)
        {
            switch (complexityConditionMulti)
            {
                case ComplexityConditionMulti.none:
                    return true;
                case ComplexityConditionMulti.overTen:
                    return state.MaxNumMulti > 10;
                case ComplexityConditionMulti.overTwenty:
                    return state.MaxNumMulti > 20;
                case ComplexityConditionMulti.overHundert:
                    return state.MaxNumMulti > 100;
                default:
                    throw new NotImplementedException("Unknown ComplexityCondition");
            }
        }

        public bool IsComplexityConditionSatisfied(OperationCondition operationCondition, ComplexityState state)
        {
            switch (operationCondition)
            {
                case OperationCondition.addition:
                    return true;
                case OperationCondition.subtraction:
                    state.OperationCondition.Contains(OperationCondition.addition);
                    return state.MaxNumAdd > 10;
                case OperationCondition.multiplication:
                    return state.OperationCondition.Contains(OperationCondition.addition)
                        && state.OperationCondition.Contains(OperationCondition.subtraction);
                case OperationCondition.division:
                    return state.OperationCondition.Contains(OperationCondition.addition)
                        && state.OperationCondition.Contains(OperationCondition.subtraction)
                        && state.OperationCondition.Contains(OperationCondition.multiplication)
                        && state.MaxNumMulti > 10;
                    ;
                default:
                    throw new NotImplementedException("Unknown ComplexityCondition");
            }
        }

        public bool IsComplexityConditionSatisfiedForMultiNumber(ComplexityState complexityState)
        {
            return complexityState.OperationCondition.Contains(OperationCondition.multiplication);
        }
    }
}
