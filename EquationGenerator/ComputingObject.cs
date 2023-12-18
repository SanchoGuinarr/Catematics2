using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    public class ComputingObject : IComputingObject, ICartItem
    {
        private int imageNumber;

        public int Complexity { get; set; }
        public string Title { get; set; }
        public int Price { get ; set; }
        public int ImageNumber { 
            get => imageNumber; 
            set 
            {
                ImageSource = $"cat_v00n{value.ToString().PadLeft(2, '0')}.jpg";
                imageNumber = value;
            }
        }
        public string ImageSource { get; set; }
        public string Description { get; set; }


        public ComplexityState ModifyState(ComplexityState state)
        {
            return state;
        }
    }
}
