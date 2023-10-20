using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.CartItems
{
    class CartItemNumber : ICartItem
    {
        public int Price { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

    class CartItemNumberAdd : CartItemNumber
    {

    }

    class CartItemNumberMulti : CartItemNumber
    {

    }
}
