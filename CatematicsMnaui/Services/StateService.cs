using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationGenerator;
using EquationGenerator.Interfaces;

namespace CatematicsMnaui.Services
{
    public class StateService
    {
        public ComplexityState State { get; set; }
        public SettingsDefinition Settings { get; set; }
        public IMoneyCounter MoneyCounter { get; set; }
        public List<IComputingObject> ComputingObjects { get; set; }

    }
}
