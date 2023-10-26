using EquationGenerator.Interfaces;
using System.Collections.Generic;

namespace EquationGenerator.Services.Interfaces
{
    public interface ICartService
    {
        List<ICartItem> CartItems { get; set; }

        bool GenerateCartItems(ComplexityState state);
    }
}