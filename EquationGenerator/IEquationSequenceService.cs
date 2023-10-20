using EquationGenerator.Interfaces;
using System.Collections.Generic;

namespace EquationGenerator
{
    public interface IEquationSequenceService
    {
        List<ICartItem> Cart { get; set; }
        IMoneyCounter Treasure { get; set; }

        void Buy(ICartItem newItem);
        ComputingObject ComputedByObject(AEquation equation);
        void EquationSolved(AEquation equation);
        void GenerateSequence(bool fullLog);
        AEquation NextEquation();
    }
}