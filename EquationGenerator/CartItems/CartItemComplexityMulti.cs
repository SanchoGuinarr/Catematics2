using EquationGenerator.Interfaces;
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

        public string Name => "Podmínka: " + Generator.ComplexityConditionMultiToString(Condition) + " (" + Price + ")";

        public ComplexityConditionMulti Condition { get; set; }
    }
}
