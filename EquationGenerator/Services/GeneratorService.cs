using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationGenerator.Services.Interfaces;

namespace EquationGenerator.Services
{
    public enum OperationCondition
    {
        addition = 0,
        subtraction = 1,
        multiplication = 2,
        division = 3
    }
    public enum ComplexityConditionAdd
    {
        none = 0,
        overTen = 1,
        overMultiTens = 2
    }

    public enum ComplexityConditionMulti
    {
        none = 0,
        overTen = 1,
        overTwenty = 2,
        overHundert = 3
    }
    public class GeneratorService : IGeneratorService
    {
        Random Random = new Random();
        public AEquation GenerateIntEquation(ComplexityState state, bool random = true)
        {
            OperationCondition operation = GenerateOperation(state.OperationCondition, random);
            int firstNumber;
            int maxComplexityNumber;
            int secondNumber;
            bool switcher;
            switch (operation)
            {
                case OperationCondition.addition:
                    firstNumber = random ? Random.Next(0, state.MaxNumAdd + 1) : state.MaxNumAdd;
                    switch (state.ComplexityConditionAdd)
                    {
                        case ComplexityConditionAdd.none:
                            maxComplexityNumber = 10 - firstNumber % 10;
                            break;
                        case ComplexityConditionAdd.overTen:
                            maxComplexityNumber = 10 - firstNumber % 10 + 10;
                            break;
                        case ComplexityConditionAdd.overMultiTens:
                            maxComplexityNumber = state.MaxNumAdd;
                            break;
                        default:
                            throw new Exception("Unknown ComplexityCondition");
                    }
                    secondNumber = random ? Random.Next(0, Math.Min(state.MaxNumAdd, maxComplexityNumber) + 1) : Math.Min(state.MaxNumAdd, maxComplexityNumber);
                    switcher = GetRandomBool();
                    return new EquationAdition()
                    {
                        Type = NumberType.integer,
                        FirstNumber = new IntNumber() { Value = switcher ? firstNumber : secondNumber },
                        SecondNumber = new IntNumber() { Value = switcher ? secondNumber : firstNumber }
                    };
                case OperationCondition.subtraction:
                    firstNumber = random ? Random.Next(0, state.MaxNumAdd + 1) : state.MaxNumAdd;
                    switch (state.ComplexityConditionAdd)
                    {
                        case ComplexityConditionAdd.none:
                            maxComplexityNumber = firstNumber % 10;
                            break;
                        case ComplexityConditionAdd.overTen:
                            maxComplexityNumber = firstNumber % 10 + 10;
                            break;
                        case ComplexityConditionAdd.overMultiTens:
                            maxComplexityNumber = firstNumber;
                            break;
                        default:
                            throw new Exception("Unknown ComplexityCondition");
                    }
                    secondNumber = random ? Random.Next(0, Math.Min(state.MaxNumAdd, maxComplexityNumber) + 1) : Math.Min(state.MaxNumAdd, maxComplexityNumber);
                    return new EquationSubtraction()
                    {
                        Type = NumberType.integer,
                        FirstNumber = new IntNumber() { Value = firstNumber },
                        SecondNumber = new IntNumber() { Value = secondNumber }
                    };
                case OperationCondition.multiplication:
                    firstNumber = random ? Random.Next(0, state.MaxNumMulti + 1) : state.MaxNumMulti;
                    switch (state.ComplexityConditionMulti)
                    {
                        case ComplexityConditionMulti.none:
                            maxComplexityNumber = 10;
                            break;
                        case ComplexityConditionMulti.overTen:
                            maxComplexityNumber = Math.Max(20, state.MaxNumMulti);
                            break;
                        case ComplexityConditionMulti.overTwenty:
                            maxComplexityNumber = Math.Max(100, state.MaxNumMulti);
                            break;
                        case ComplexityConditionMulti.overHundert:
                            maxComplexityNumber = Math.Max(1000, state.MaxNumMulti);
                            break;
                        default:
                            throw new Exception("Unknown ComplexityCondition");
                    }
                    secondNumber = random ? Random.Next(0, Math.Min(state.MaxNumAdd, maxComplexityNumber) + 1) : Math.Min(state.MaxNumAdd, maxComplexityNumber);
                    switcher = GetRandomBool();
                    return new EquationMultiplication()
                    {
                        Type = NumberType.integer,
                        FirstNumber = new IntNumber() { Value = switcher ? firstNumber : secondNumber },
                        SecondNumber = new IntNumber() { Value = switcher ? secondNumber : firstNumber }
                    };
                case OperationCondition.division:
                    firstNumber = random ? Random.Next(0, state.MaxNumMulti + 1) : state.MaxNumMulti;
                    switch (state.ComplexityConditionMulti)
                    {
                        case ComplexityConditionMulti.none:
                            maxComplexityNumber = 10;
                            break;
                        case ComplexityConditionMulti.overTen:
                            maxComplexityNumber = Math.Max(20, state.MaxNumMulti);
                            break;
                        case ComplexityConditionMulti.overTwenty:
                            maxComplexityNumber = Math.Max(100, state.MaxNumMulti);
                            break;
                        case ComplexityConditionMulti.overHundert:
                            maxComplexityNumber = Math.Max(1000, state.MaxNumMulti);
                            break;
                        default:
                            throw new Exception("Unknown ComplexityCondition");
                    }
                    secondNumber = random ? Random.Next(0, Math.Min(state.MaxNumAdd, maxComplexityNumber) + 1) : Math.Min(state.MaxNumAdd, maxComplexityNumber);
                    switcher = GetRandomBool();
                    return new EquationDivision()
                    {
                        Type = NumberType.integer,
                        FirstNumber = new IntNumber() { Value = firstNumber * secondNumber },
                        SecondNumber = new IntNumber() { Value = switcher ? firstNumber : secondNumber },
                        Result = new IntNumber() { Value = switcher ? secondNumber : firstNumber },
                    };
                default:
                    throw new Exception("Unknown Operation condition");
            }
        }

        private OperationCondition GenerateOperation(HashSet<OperationCondition> operationCondition, bool random)
        {
            if (random)
            {
                int position = Random.Next(0, operationCondition.Count);
                return operationCondition.ElementAt(position);
            }
            else
            {
                return operationCondition.ElementAt(operationCondition.Count - 1);
            }
        }

        private bool GetRandomBool()
        {
            return Random.Next(0, 2) == 1;
        }

        public static string OperationToString(OperationCondition type)
        {
            switch (type)
            {
                case OperationCondition.addition:
                    return "Sčítání";
                case OperationCondition.subtraction:
                    return "Odčítání";
                case OperationCondition.multiplication:
                    return "Násobení";
                case OperationCondition.division:
                    return "Dělení";
                default:
                    throw new Exception("Unknown OperationCondition");
            }
        }

        public static string ComplexityConditionAddToString(ComplexityConditionAdd type)
        {
            switch (type)
            {
                case ComplexityConditionAdd.none:
                    return "Žádná";
                case ComplexityConditionAdd.overTen:
                    return "Sčítání přes desítku";
                case ComplexityConditionAdd.overMultiTens:
                    return "Sčítání přes více desítek";
                default:
                    throw new Exception("Unknown ComplexityConditionAdd");
            }
        }

        public static string ComplexityConditionMultiToString(ComplexityConditionMulti type)
        {
            switch (type)
            {
                case ComplexityConditionMulti.none:
                    return "Žádná";
                case ComplexityConditionMulti.overTen:
                    return "Malá násobilka";
                case ComplexityConditionMulti.overTwenty:
                    return "Velká násobilka";
                case ComplexityConditionMulti.overHundert:
                    return "Násobení bez omezení";
                default:
                    throw new Exception("Unknown ComplexityConditionMulti");
            }
        }
    }
}
