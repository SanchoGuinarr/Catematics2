using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.Interfaces
{
    public interface IComputingObject
    {
        public int Complexity { get; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
