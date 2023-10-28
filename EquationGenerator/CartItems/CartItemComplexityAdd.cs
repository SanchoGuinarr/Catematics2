using EquationGenerator.Interfaces;
using EquationGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.CartItems
{
    class CartItemComplexityAdd: ICartItem
    {
        public int Price { get; set; }

        public string Title => "Pokročilé sčítání";

        public string Description => "Podmínka: " + GeneratorService.ComplexityConditionAddToString(Condition);

        public string ImageSource => "shopping_cart_black_24dp.svg";

        public ComplexityConditionAdd Condition { get; set; }

        public ComplexityState ModifyState(ComplexityState state)
        {
            state.ComplexityConditionAdd = Condition;
            return state;
        }
    }
}
