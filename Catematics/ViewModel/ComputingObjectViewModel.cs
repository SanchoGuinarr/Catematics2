using EquationGenerator;
using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catematics.ViewModel
{
    class ComputingObjectViewModel
    {
        private IComputingObject model;

        public string Name => model.Name;
        public string ImageSource { get; set; }
        public ComputingObjectViewModel(ComputingObject Model)
        {
            model = Model;
        }
    }
}
