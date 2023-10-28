using EquationGenerator.Interfaces;
using EquationGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.Services
{
    public class ComputingObjectService : IComputingObjectService
    {
        /// <summary>
        /// Chance of success is one in specified number, so if SUCCESS_CHANCE is 3, then chance of success is 1/3
        /// </summary>
        private const int SUCCESS_CHANCE = 3;

        private readonly Random _random = new();

        public ComputingObjectService()
        {
        }

        public List<IComputingObject> ComputingObjects { get; set; } = new List<IComputingObject>();

        public IComputingObject GetEquationSolver(IEquation equation)
        {
            foreach (IComputingObject computingObject in ComputingObjects)
            {
                if (equation.Complexity < computingObject.Complexity)
                {
                    if (_random.Next(SUCCESS_CHANCE) < 1)
                    {
                        return computingObject;
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
