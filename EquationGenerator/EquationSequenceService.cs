using EquationGenerator.CartItems;
using EquationGenerator.Interfaces;
using EquationGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    public class EquationSequenceService : IEquationSequenceService
    {
        private SettingsDefinition settings;
        public List<ICartItem> Cart { get; set; } = new();
        public List<ComputingObject> ComputingObjects { get; set; } = new();

        private ComplexityState actualState;

        private Random Random = new();
        private GeneratorService generator = new();

        public IMoneyCounter Treasure { get; set; } = new MoneyCounter();
        private int counter = 0;

        private int nameCounter = 0;

        private int computingValueStep;
        private int nextComputingObjectValue;

        private int operationValueStep;
        private int nextOperationValue;

        private int complexityAddValueStep;
        private int nextComplexityAddValue;

        private int complexityMultiValueStep;
        private int nextComplexityMultiValue;

        // helpers for logging
        private string computed = "";
        private int computedCounter = 0;
        public EquationSequenceService(SettingsDefinition Settings)
        {
            settings = Settings;
            Init();
        }
        public EquationSequenceService()
        {
            settings = new()
            {
                ComputingObjectsCount = 10,
                //EquationStepMin = 5,
                //EquationStepMax = 15,
                InitialState = GetInitialState(),
                FinalState = new()
                {
                    MaxNumAdd = 40,
                    MaxNumMulti = 20,
                    OperationCondition = new() { OperationCondition.addition, OperationCondition.subtraction, OperationCondition.multiplication, OperationCondition.division },
                    ComplexityConditionAdd = ComplexityConditionAdd.overMultiTens,
                    ComplexityConditionMulti = ComplexityConditionMulti.overTwenty
                },
            };
            Init();
        }

        private ComplexityState GetInitialState()
        {
            return new()
            {
                MaxNumAdd = 0,
                MaxNumMulti = 0,
                OperationCondition = new() { OperationCondition.addition },
                ComplexityConditionAdd = ComplexityConditionAdd.none,
                ComplexityConditionMulti = ComplexityConditionMulti.none
            };
        }

        private void Init()
        {
            actualState = settings.InitialState;

            int addNumSteps = settings.FinalState.MaxNumAdd - settings.InitialState.MaxNumAdd;

            computingValueStep = addNumSteps / settings.ComputingObjectsCount;
            nextComputingObjectValue = computingValueStep;

            if (settings.FinalState.OperationCondition.Count != settings.InitialState.OperationCondition.Count)
            {
                operationValueStep = addNumSteps / (settings.FinalState.OperationCondition.Count - settings.InitialState.OperationCondition.Count);
                nextOperationValue = operationValueStep;
            }
            else
            {
                nextOperationValue = settings.FinalState.MaxNumAdd + 1;
            }

            if (settings.FinalState.ComplexityConditionAdd != settings.InitialState.ComplexityConditionAdd)
            {
                complexityAddValueStep = addNumSteps / (settings.FinalState.ComplexityConditionAdd - settings.InitialState.ComplexityConditionAdd);
                nextComplexityAddValue = complexityAddValueStep;
            }
            else
            {
                nextComplexityAddValue = settings.FinalState.MaxNumAdd + 1;
            }

            int multiNumSteps = settings.FinalState.MaxNumMulti - settings.InitialState.MaxNumMulti;

            if (settings.FinalState.ComplexityConditionMulti != settings.InitialState.ComplexityConditionMulti)
            {
                complexityMultiValueStep = multiNumSteps / (settings.FinalState.ComplexityConditionMulti - settings.InitialState.ComplexityConditionMulti);
                nextComplexityMultiValue = complexityMultiValueStep;
            }
            else
            {
                nextComplexityMultiValue = settings.FinalState.MaxNumMulti + 1;
            }

            AddNewItemsToCart();
        }

        public void GenerateSequence(bool fullLog)
        {
            counter = 0;
            computedCounter = 0;
            Treasure.Money = 0;
            Cart.Clear();
            ComputingObjects.Clear();
            settings.InitialState = GetInitialState();
            Init();
            while (actualState.MaxNumAdd < settings.FinalState.MaxNumAdd && counter < 1000)
            {
                counter++;
                AEquation equation = NextEquation();

                string computedString = ComputedByObjectString(equation);
                if (computedString == "")
                {
                    computedCounter++;
                    computed += computedCounter.ToString() + ": " + equation.GetWholeEquation() + Environment.NewLine;
                }
                if (fullLog)
                {
                    Console.WriteLine(computedString + counter.ToString() + ": " + equation.GetWholeEquation() + " Price: " + equation.Reward + " Complexity: " + equation.Complexity + " Money: " + Treasure);
                    Console.WriteLine(string.Join(';', Cart));
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                }
                EquationSolved(equation);
                Buy();
            }
            if (fullLog)
            {
                Console.Write(computed);
            }
            Console.WriteLine("Vygenerováno: " + counter + " Spočítáno ručně: " + computedCounter + " Spočítáno automaticky: " + (counter - computedCounter).ToString());
        }

        public AEquation NextEquation()
        {
            AEquation equation = generator.GenerateIntEquation(actualState);
            return equation;
        }

        public void EquationSolved(AEquation equation)
        {
            Treasure.Money += equation.Reward;
            AddNewItemsToCart();
        }

        private string ComputedByObjectString(AEquation equation)
        {
            var computingObject = ComputedByObject(equation);
            if (computingObject is null)
            {
                return "";
            }
            else
            {
                return "COMPUTED BY: " + computingObject.Name + " ";
            }
        }

        public ComputingObject ComputedByObject(AEquation equation)
        {
            foreach (var computingObject in ComputingObjects)
            {
                if (equation.Complexity < computingObject.Complexity)
                {
                    if (Random.Next(10) < 3)
                    {
                        return computingObject;
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public void Buy(ICartItem newItem)
        {
            Treasure.Money -= newItem.Price;
            if (newItem is ComputingObject computingObject)
            {
                ComputingObjects.Add(computingObject);
            }
            if (newItem is CartItemNumberAdd)
            {
                actualState.MaxNumAdd++;
            }
            else if (newItem is CartItemNumberMulti)
            {
                actualState.MaxNumMulti++;
            }
            else if (newItem is CartItemOperation newOperation)
            {
                actualState.OperationCondition.Add(newOperation.Operation);
            }
            else if (newItem is CartItemComplexityAdd complexityAdd)
            {
                actualState.ComplexityConditionAdd = complexityAdd.Condition;
            }
            else if (newItem is CartItemComplexityMulti complexityMulti)
            {
                actualState.ComplexityConditionMulti = complexityMulti.Condition;
            }
            Cart.Remove(newItem);
        }
        private void Buy()
        {
            while (Cart.Count > 0 && Cart[0].Price < Treasure.Money)
            {
                ICartItem newItem = Cart[0];
                Buy(newItem);
            }
        }

        private void AddNewItemsToCart()
        {
            int maxComplexity = GenerateMaxComplexity();
            int basePrice = GenerateBasePrice(maxComplexity);
            if (actualState.MaxNumAdd > nextComputingObjectValue && Cart.Count < (settings.ComputingObjectsCount - 1) && !Cart.Any(i => i is ComputingObject))
            {
                Cart.Add(new ComputingObject()
                {
                    Complexity = maxComplexity / 4,
                    Name = "OBJECT_" + nameCounter++,
                    Price = basePrice * 3
                });
                nextComputingObjectValue += computingValueStep;
            }

            if (!Cart.Any(i => i is CartItemNumberAdd))
            {
                Cart.Add(new CartItemNumberAdd()
                {
                    Value = actualState.MaxNumAdd + 1,
                    Price = basePrice
                });
            }

            if (actualState.MaxNumAdd > nextOperationValue && !Cart.Any(i => i is CartItemOperation))
            {
                Cart.Add(new CartItemOperation()
                {
                    Operation = (OperationCondition)actualState.OperationCondition.Count,
                    Price = basePrice * 2
                });
                nextOperationValue += operationValueStep;
            }

            if (actualState.OperationCondition.Contains(OperationCondition.multiplication) && !Cart.Any(i => i is CartItemNumberMulti))
            {
                Cart.Add(new CartItemNumberMulti()
                {
                    Value = actualState.MaxNumMulti + 1,
                    Price = basePrice
                });
            }

            if (actualState.MaxNumAdd > nextComplexityAddValue && !Cart.Any(i => i is CartItemComplexityAdd))
            {
                Cart.Add(new CartItemComplexityAdd()
                {
                    Condition = (ComplexityConditionAdd)((int)actualState.ComplexityConditionAdd + 1),
                    Price = basePrice * 2
                });
                nextComplexityAddValue += complexityAddValueStep;
            }

            if (actualState.OperationCondition.Contains(OperationCondition.multiplication) && actualState.MaxNumMulti > nextComplexityMultiValue && !Cart.Any(i => i is CartItemComplexityMulti))
            {
                Cart.Add(new CartItemComplexityMulti()
                {
                    Condition = (ComplexityConditionMulti)((int)actualState.ComplexityConditionMulti + 1),
                    Price = basePrice * 2
                });
                nextComplexityMultiValue += complexityMultiValueStep;
            }

        }

        private int GenerateBasePrice(int maxComplexity)
        {
            return Random.Next(maxComplexity / 2, maxComplexity);
        }

        private int GenerateMaxComplexity()
        {
            AEquation equation = generator.GenerateIntEquation(actualState, false);
            return equation.Complexity;
        }
    }
}
