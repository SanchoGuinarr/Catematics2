using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    public class SettingsDefinition
    {
        public ComplexityState InitialState { get; set; }
        public ComplexityState FinalState { get; set; }
        public int ComputingObjectsCount { get; set; }
    }
}
