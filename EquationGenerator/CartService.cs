using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationGenerator;
using EquationGenerator.CartItems;
using EquationGenerator.Interfaces;
using EquationGenerator.Services;

namespace EquationGenerator
{
    public class CartService
    {
        /// <summary>
        /// Solving complexity is determined by complexity of the equation and this modifier
        /// </summary>
        private const int COMPUTING_OBJECT_COMPLEXITY_MODIFIER = 4;
        /// <summary>
        /// Basic price is determined by complexity of the equation and this modifier
        /// </summary>
        private const int PRICE_MODIFIER = 2;

        private int _computingObjectCounter = 1;

        private readonly ISettingsService _settingsService;
        private readonly IComplexityStateService _complexityStateService;

        private readonly Random _random;

        public List<ICartItem> CartItems { get; set; }

        /// <summary>
        /// Generates cart items from current state
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool GenerateCartItems (ComplexityState state)
        {
            bool isGenerated = false;
            int currentComplexity = _complexityStateService.GetCurrentStateComplexity(state);
            if (!CartItems.Any(i => i is ComputingObject) && _complexityStateService.CanGenerateNextComputingObject(state, CartItems.Count))
            {
                CartItems.Add(new ComputingObject()
                {
                    Complexity = currentComplexity,
                    Name = "Kočka " + _computingObjectCounter++,
                    Price = GetBasePrice(currentComplexity) * 3, 
                });
                isGenerated = true;
            }

            if (!CartItems.Any(i => i is CartItemNumberAdd))
            {
                CartItems.Add(new CartItemNumberAdd()
                {
                    Value = state.MaxNumAdd + 1,
                    Price = GetBasePrice(currentComplexity),
                });
                isGenerated = true;
            }

            if (!CartItems.Any(i => i is CartItemNumberMulti && _complexityStateService.IsComplexityConditionSatisfiedForMultiNumber(state)))
            {
                CartItems.Add(new CartItemNumberMulti()
                {
                    Value = state.MaxNumMulti + 1,
                    Price = GetBasePrice(currentComplexity),
                });
            }

            OperationCondition nextOperation = (OperationCondition)state.OperationCondition.Count;
            if (!CartItems.Any(i => i is CartItemOperation) && _complexityStateService.IsComplexityConditionSatisfied(nextOperation, state))
            {
                CartItems.Add(new CartItemOperation()
                {
                    Operation = nextOperation,
                    Price = GetBasePrice(currentComplexity) * 2,
                });
            }

            // TODO: continue here

            return isGenerated;
        }

        /// <summary>
        /// Generates cart items from current state and obtained computing objects
        /// </summary>
        /// <param name="state"></param>
        /// <param name="computinObjects"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void GenerateCartItems (ComplexityState state, List<ComputingObject> computinObjects)
        {
            throw new NotImplementedException();
        }

        private IComputingObject GenerateNextComputingItem(int currentComplexity)
        {
            return new ComputingObject()
            {
                Complexity = currentComplexity,
                Name = "Kočka " + _computingObjectCounter,
                Price = GetBasePrice(currentComplexity),
            };
        }

        private int GetBasePrice(int complexity)
        {
            return _random.Next(complexity / PRICE_MODIFIER, complexity);
        }
    }
}
