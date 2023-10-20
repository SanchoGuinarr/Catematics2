using System;

namespace EquationGenerator
{
    public enum NumberType 
    {
        integer,
        fraction
    }

    public abstract class AEquation
    {
        public NumberType Type { get; set; }
        public ANumber FirstNumber { get; set; }
        public ANumber SecondNumber { get; set; }

        private ANumber result;
        private int? complexity;
        private int? reward;

        private Random random = new();
        protected abstract int ComplexityModificator { get; }

        public ANumber Result
        {
            get
            {
                if (result is null)
                {
                    result = ComputeResult();
                }
                return result;
            }
            set
            {
                result = value;
            }
        }

        public int Complexity
        {
            get
            {
                if (complexity is null)
                {
                    complexity = ComputeComplexity();
                }
                return complexity ?? 0;
            }
            set
            {
                complexity = value;
            }
        }

        public int Reward
        {
            get
            {
                if (reward is null)
                {
                    reward = random.Next(1, Complexity < 1 ? 1 : Complexity);
                }
                return reward ?? 1;
            }
        }

        protected abstract string operatorString { get; }
        public string GetAssignment()
        {
            return FirstNumber.ToString() + " " + operatorString + " " + SecondNumber.ToString() + " = ";
        }
        public string GetWholeEquation() 
        {
            return GetAssignment() + " " + Result.ToString();
        }

        public bool CheckResult(int result)
        {
            if (Result is IntNumber intNumber)
            {
                return intNumber.Value == result;
            }
            else
            {
                return false;
            }
        }

        public bool CheckResult(string result)
        {
            // TODO: fraction in format X/Z
            int resultNum;
            try
            {
                resultNum = Convert.ToInt32(result);
            }
            catch
            {
                return false;
            }

            if (Result is IntNumber intNumber)
            {
                return intNumber.Value == resultNum;
            }
            else
            {
                return false;
            }
        }

        protected int ComputeComplexity() 
        {
            return FirstNumber.GetComplexityValue() * SecondNumber.GetComplexityValue() * Result.GetComplexityValue();
        }
        protected abstract ANumber ComputeResult();

        protected ANumber SimplifyFraction(FractionNumber number)
        {
            int highestDenominator = 1;
            int highestPossible = Math.Max(number.Denominator, number.Numerator) / 2;
            for (int i = 1; i < highestPossible; i++)
            {
                if (number.Denominator % i == 0 && number.Numerator % i == 0)
                {
                    highestDenominator = i;
                }
            }
            number.Numerator /= highestDenominator;
            number.Denominator /= highestDenominator;
            if (number.Numerator % number.Denominator == 0)
            {
                return new IntNumber()
                {
                    Value = number.Numerator / number.Denominator 
                };
            }
            else
            {
                return number;
            }
        }

       
    }

    public class EquationAdition : AEquation
    {
        protected override int ComplexityModificator => 1;

        protected override string operatorString => "+";

        protected override ANumber ComputeResult()
        {
            if (FirstNumber is IntNumber int1 && SecondNumber is IntNumber int2)
            {
                return new IntNumber()
                {
                    Value = int1.Value + int2.Value
                };
            }
            if (FirstNumber is FractionNumber fraction1 && SecondNumber is FractionNumber fraction2)
            {
                FractionNumber result = new()
                {
                    Numerator = (fraction1.Numerator * fraction2.Denominator) + (fraction2.Numerator * fraction1.Denominator),
                    Denominator = fraction1.Denominator * fraction2.Denominator
                };
                return SimplifyFraction(result);
            }
            throw new Exception("Inconsistent or unknown value types.");
        }
        
    }

    public class EquationSubtraction : AEquation
    {
        protected override string operatorString => "-";
        protected override int ComplexityModificator => 2;
        protected override ANumber ComputeResult()
        {
            if (FirstNumber is IntNumber int1 && SecondNumber is IntNumber int2)
            {
                return new IntNumber()
                {
                    Value = int1.Value - int2.Value
                };
            }
            if (FirstNumber is FractionNumber fraction1 && SecondNumber is FractionNumber fraction2)
            {
                FractionNumber result = new()
                {
                    Numerator = (fraction1.Numerator * fraction2.Denominator) - (fraction2.Numerator * fraction1.Denominator),
                    Denominator = fraction1.Denominator * fraction2.Denominator
                };
                return SimplifyFraction(result);
            }
            throw new Exception("Inconsistent or unknown value types.");
        }
    }

    public class EquationMultiplication : AEquation
    {
        protected override string operatorString => "*";
        protected override int ComplexityModificator => 3;
        protected override ANumber ComputeResult()
        {
            if (FirstNumber is IntNumber int1 && SecondNumber is IntNumber int2)
            {
                return new IntNumber()
                {
                    Value = int1.Value * int2.Value
                };
            }
            if (FirstNumber is FractionNumber fraction1 && SecondNumber is FractionNumber fraction2)
            {
                FractionNumber result = new()
                {
                    Numerator = fraction1.Numerator * fraction2.Numerator,
                    Denominator = fraction1.Denominator * fraction2.Denominator
                };
                return SimplifyFraction(result);
            }
            throw new Exception("Inconsistent or unknown value types.");
        }
    }

    public class EquationDivision : AEquation
    {
        protected override string operatorString => ":";
        protected override int ComplexityModificator => 4;
        protected override ANumber ComputeResult()
        {
            if (FirstNumber is IntNumber int1 && SecondNumber is IntNumber int2)
            {
                return new IntNumber()
                {
                    Value = int1.Value / int2.Value
                };
            }
            if (FirstNumber is FractionNumber fraction1 && SecondNumber is FractionNumber fraction2)
            {
                FractionNumber result = new()
                {
                    Numerator = fraction1.Numerator * fraction2.Denominator,
                    Denominator = fraction1.Denominator * fraction2.Numerator
                };
                return SimplifyFraction(result);
            }
            throw new Exception("Inconsistent or unknown value types.");
        }
    }
}
