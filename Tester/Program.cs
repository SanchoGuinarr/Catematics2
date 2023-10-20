using EquationGenerator;
using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            EquationSequence sequence = new();
            for (int i = 0; i < 10; i++)
            {
                sequence.GenerateSequence(true);
            }
        }
    }
}
