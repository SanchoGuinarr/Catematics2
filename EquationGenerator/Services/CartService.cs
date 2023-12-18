using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatematicsMnaui.Services;
using EquationGenerator.CartItems;
using EquationGenerator.Interfaces;
using EquationGenerator.Services.Interfaces;

namespace EquationGenerator.Services
{
    public class CartService : ICartService
    {
        private const int COMPUTING_OBJECT_NUMBER = 16;

        /// <summary>
        /// Solving complexity is determined by complexity of the equation and this modifier
        /// </summary>
        private const int COMPUTING_OBJECT_COMPLEXITY_MODIFIER = 4;
        /// <summary>
        /// Basic price is determined by complexity of the equation and this modifier
        /// </summary>
        private const int BASIC_PRICE_MODIFIER = 2;

        // price modifiers
        private const int COMPUTING_OBJECT_PRICE_MODIFIER = 3;
        private const int NUMBER_ITEM_ADD_PRICE_MODIFIER = 1;
        private const int NUMBER_ITEM_MULTI_PRICE_MODIFIER = 1;
        private const int OPERATION_PRICE_MODIFIER = 2;
        private const int COMPLEXITY_ADD_PRICE_MODIFIER = 2;
        private const int COMPLEXITY_MULTI_PRICE_MODIFIER = 2;

        private int _computingObjectCounter = 1;
        // used for determining computing object description
        private int _maxLevel = 0;

        private readonly ISettingsService _settingsService;
        private readonly IComplexityStateService _complexityStateService;
        private readonly IComputingObjectService _computingObjectService;

        private readonly Random _random;
        private Queue<int> _computingObjectImages = new();

        public List<ICartItem> CartItems { get; set; } = new();

        public CartService(ISettingsService settingsService, IComplexityStateService complexityStateService, IComputingObjectService computingObjectService)
        {
            _settingsService = settingsService;
            _complexityStateService = complexityStateService;
            _computingObjectService = computingObjectService;

            _random = new Random();

            _maxLevel = _settingsService.Settings.ComputingObjectsCount;

            // generate random images for computing objects - no image is used twice
            for (int i = 0; i < settingsService.Settings.ComputingObjectsCount; i++)
            {
                int image = _random.Next(COMPUTING_OBJECT_NUMBER);
                while (_computingObjectImages.Contains(image))
                {
                    image = _random.Next(COMPUTING_OBJECT_NUMBER);
                }
                _computingObjectImages.Enqueue(image);
            }
        }

        /// <summary>
        /// Generates cart items from current state
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool GenerateCartItems(ComplexityState state)
        {
            bool isGenerated = false;
            int currentComplexity = _complexityStateService.GetCurrentStateComplexity(state);

            // generate computing objects
            isGenerated = GenerateComputingObjects(state, isGenerated, currentComplexity);

            // generate number items - number add
            isGenerated = GenerateNumberItemAdd(state, isGenerated, currentComplexity);

            // generate number items - number multi
            isGenerated = GenerateNumberItemMulti(state, isGenerated, currentComplexity);

            // generate operation
            isGenerated = GenereteNextOperation(state, isGenerated, currentComplexity);

            // generate complexity add
            isGenerated = GenerateComplexityAdd(state, isGenerated, currentComplexity);

            // generate complexity multi
            isGenerated = GenerateComplexityMulti(state, isGenerated, currentComplexity);

            return isGenerated;
        }

        private bool GenerateComplexityMulti(ComplexityState state, bool isGenerated, int currentComplexity)
        {
            if ((int)state.ComplexityConditionMulti + 1 >= Enum.GetNames(typeof(ComplexityConditionMulti)).Length)
            {
                return isGenerated;
            }
            ComplexityConditionMulti nextComplexity = (ComplexityConditionMulti)((int)state.ComplexityConditionMulti + 1);
            if ((!CartItems.Any(i => i is CartItemComplexityMulti)) && _complexityStateService.IsComplexityConditionSatisfied(nextComplexity, state))
            {
                CartItems.Add(new CartItemComplexityMulti()
                {
                    Condition = nextComplexity,
                    Price = GetBasePrice(currentComplexity) * COMPLEXITY_MULTI_PRICE_MODIFIER,
                });
                isGenerated = true;
            }
            return isGenerated;
        }

        private bool GenerateComplexityAdd(ComplexityState state, bool isGenerated, int currentComplexity)
        {
            if ((int)state.ComplexityConditionAdd + 1 >= Enum.GetNames(typeof(ComplexityConditionAdd)).Length)
            {
                return isGenerated;
            }
            ComplexityConditionAdd nextComplexity = (ComplexityConditionAdd)((int)state.ComplexityConditionAdd + 1);
            if ((!CartItems.Any(i => i is CartItemComplexityAdd)) && _complexityStateService.IsComplexityConditionSatisfied(nextComplexity, state))
            {
                CartItems.Add(new CartItemComplexityAdd()
                {
                    Condition = nextComplexity,
                    Price = GetBasePrice(currentComplexity) * COMPLEXITY_ADD_PRICE_MODIFIER,
                });
                isGenerated = true;
            }
            return isGenerated;
        }

        private bool GenereteNextOperation(ComplexityState state, bool isGenerated, int currentComplexity)
        {
            // check if operation can be generated (curent count must be less than OperationCondition enum count)
            if (state.OperationCondition.Count >= Enum.GetNames(typeof(OperationCondition)).Length)
            {
                return isGenerated;
            }

            OperationCondition nextOperation = (OperationCondition)state.OperationCondition.Count;
            if ((!CartItems.Any(i => i is CartItemOperation)) && _complexityStateService.IsComplexityConditionSatisfied(nextOperation, state))
            {
                CartItems.Add(new CartItemOperation()
                {
                    Operation = nextOperation,
                    Price = GetBasePrice(currentComplexity) * OPERATION_PRICE_MODIFIER,
                });
                isGenerated = true;
            }

            return isGenerated;
        }

        private bool GenerateNumberItemMulti(ComplexityState state, bool isGenerated, int currentComplexity)
        {
            if ((!CartItems.Any(i => i is CartItemNumberMulti) && _complexityStateService.IsComplexityConditionSatisfiedForMultiNumber(state)))
            {
                CartItems.Add(new CartItemNumberMulti()
                {
                    Value = state.MaxNumMulti + 1,
                    Price = GetBasePrice(currentComplexity) * NUMBER_ITEM_MULTI_PRICE_MODIFIER,
                });
                isGenerated = true;
            }

            return isGenerated;
        }

        private bool GenerateNumberItemAdd(ComplexityState state, bool isGenerated, int currentComplexity)
        {
            if (!CartItems.Any(i => i is CartItemNumberAdd))
            {
                CartItems.Add(new CartItemNumberAdd()
                {
                    Value = state.MaxNumAdd + 1,
                    Price = GetBasePrice(currentComplexity) * NUMBER_ITEM_ADD_PRICE_MODIFIER,
                });
                isGenerated = true;
            }

            return isGenerated;
        }

        private bool GenerateComputingObjects(ComplexityState state, bool isGenerated, int currentComplexity)
        {
            if ((!CartItems.Any(i => i is ComputingObject)) && _complexityStateService.CanGenerateNextComputingObject(state, _computingObjectService.ComputingObjects.Count))
            {
                CartItems.Add(new ComputingObject()
                {
                    Complexity = currentComplexity / COMPUTING_OBJECT_COMPLEXITY_MODIFIER,
                    Title = "Kočka " + _computingObjectService.ComputingObjects.Count,
                    Description = "Kočka " + NamesHelper.GetCurentLevelName(_computingObjectService.ComputingObjects.Count, _settingsService.Settings.ComputingObjectsCount),
                    Price = GetBasePrice(currentComplexity) * COMPUTING_OBJECT_PRICE_MODIFIER,
                    ImageNumber = _computingObjectImages.Dequeue(),
                });
                isGenerated = true;
            }

            return isGenerated;
        }

        private int GetBasePrice(int complexity)
        {
            return _random.Next(complexity / BASIC_PRICE_MODIFIER, complexity);
        }
    }
}
