using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.CartItems
{
    class CartItemNumberAdd : ICartItem
    {
        public int Price { get; set; }
        public string Title => "Číslo " + Value.ToString();
        public string Description => $"Číslo {Value} pro sčítání a odčítání.";
        public string ImageSource => "num.jpg";
        public int Value { get; set; }
        public ComplexityState ModifyState(ComplexityState state)
        {
            state.MaxNumAdd = Value;
            return state;
        }

    }

    class CartItemNumberMulti : ICartItem
    {
        public int Price { get; set; }
        public string Title => "Číslo " + Value.ToString() + " (*)";
        public string Description => $"Číslo {Value} pro násobení a dělení.";
        public string ImageSource => "num_multi.jpg";
        public int Value { get; set; }
        public ComplexityState ModifyState(ComplexityState state)
        {
            state.MaxNumMulti = Value;
            return state;
        }
    }
}
