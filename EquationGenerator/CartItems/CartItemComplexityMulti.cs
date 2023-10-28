using EquationGenerator.Interfaces;
using EquationGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.CartItems
{
    class CartItemComplexityMulti : ICartItem
    {
        public int Price { get; set; }

        public string Title => "Pokročilé násobení";

        public string Description => "Podmínka: " + GeneratorService.ComplexityConditionMultiToString(Condition);

        public string ImageSource => "shopping_cart_black_24dp.svg";

        public ComplexityConditionMulti Condition { get; set; }

        public ComplexityState ModifyState(ComplexityState state)
        {
            state.ComplexityConditionMulti = Condition;
            return state;
        }
    }
}
