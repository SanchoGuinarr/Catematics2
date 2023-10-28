using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.Interfaces
{
    public interface ICartItem
    {
        public int Price { get; set; }
        public string Title { get; }
        public string Description { get; }
        public string ImageSource { get; }
        public ComplexityState ModifyState(ComplexityState state);
    }
}
