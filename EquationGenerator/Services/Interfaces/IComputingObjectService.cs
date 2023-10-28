using EquationGenerator.Interfaces;
using System.Collections.Generic;

namespace EquationGenerator.Services.Interfaces
{
    public interface IComputingObjectService
    {
        List<IComputingObject> ComputingObjects { get; set; }

        IComputingObject GetEquationSolver(IEquation equation);
    }
}