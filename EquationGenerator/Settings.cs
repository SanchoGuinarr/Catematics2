using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    public class Settings
    {
        public State InitialState { get; set; }
        public State FinalState { get; set; }
        public int ComputingObjectsCount { get; set; }
        public int EquationStepMin { get; set; }
        public int EquationStepMax { get; set; }
    }
}
