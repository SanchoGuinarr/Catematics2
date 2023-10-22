namespace EquationGenerator.Services
{
    public interface IComplexityStateService
    {
        bool CanGenerateNextComputingObject(ComplexityState state, int existedComputingOjects);
        int GetCurrentStateComplexity(ComplexityState state);
        bool IsComplexityConditionSatisfied(ComplexityConditionAdd complexityConditionAdd, ComplexityState state);
        bool IsComplexityConditionSatisfied(ComplexityConditionMulti complexityConditionMulti, ComplexityState state);
        bool IsComplexityConditionSatisfied(OperationCondition operationCondition, ComplexityState state);
        bool IsComplexityConditionSatisfiedForMultiNumber(ComplexityState complexityState);
    }
}