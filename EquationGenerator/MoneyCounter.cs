using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    public class MoneyCounter : IMoneyCounter
    {
        public int Money { get; set; } = 0;
    }
}
