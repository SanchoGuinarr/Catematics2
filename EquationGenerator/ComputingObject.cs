using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    public class ComputingObject: IComputingObject, ICartItem
    {
        public int Complexity { get; set; }
        public string Name { get; set; }
        public int Price { get ; set; }
        public string ImageSource { get; set; }
    }
}
