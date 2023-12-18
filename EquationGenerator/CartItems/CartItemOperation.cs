using EquationGenerator.Interfaces;
using EquationGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.CartItems
{
    class CartItemOperation : ICartItem
    {
        public int Price { get; set; }
        public string Title => GeneratorService.OperationToString(Operation);
        public string Description => "Operace: " + GeneratorService.OperationToString(Operation);
        public string ImageSource => "opeators.jpg";
        public OperationCondition Operation { get; set; }
        public ComplexityState ModifyState(ComplexityState state)
        {
            state.OperationCondition.Add(Operation);
            return state;
        }
    }
}
