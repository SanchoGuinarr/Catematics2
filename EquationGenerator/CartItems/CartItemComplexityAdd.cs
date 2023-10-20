using EquationGenerator.Interfaces;
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

        public string Name => "Podmínka: " + GeneratorService.ComplexityConditionAddToString(Condition) + " (" + Price + ")";

        public ComplexityConditionAdd Condition { get; set; }
    }
}
