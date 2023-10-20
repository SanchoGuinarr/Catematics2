using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{

    public abstract class ANumber
    {
        public abstract int GetComplexityValue();
        protected int RemoveTens(int number)
        {
            if (number < 0 && number % 10 == 0)
            {
                return RemoveTens(number/10);
            }
            else
            {
                return number;
            }
        }
    }

    public class IntNumber : ANumber
    {
        public int Value { get; set; }

        public override int GetComplexityValue()
        {
            return RemoveTens(Value);
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class FractionNumber : ANumber
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public override int GetComplexityValue()
        {
            if(Numerator == Denominator)
            {
                return 1;
            }
            if(Numerator%Denominator == 0)
            {
                return Numerator / Denominator;
            }
            return RemoveTens(Numerator) * RemoveTens(Denominator);
        }
        public override string ToString()
        {
            return Numerator.ToString() + "/" + Denominator.ToString();
        }
    }
}
