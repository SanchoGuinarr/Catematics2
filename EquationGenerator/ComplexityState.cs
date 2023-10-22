using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquationGenerator.Services;

namespace EquationGenerator
{
    public class ComplexityState
    {
        public int MaxNumAdd { get; set; }
        public int MaxNumMulti { get; set; }
        public HashSet<OperationCondition> OperationCondition { get; set; }
        public ComplexityConditionAdd ComplexityConditionAdd { get; set; }
        public ComplexityConditionMulti ComplexityConditionMulti { get; set; }
        public int GetNumberStepValue()
        {
            return MaxNumAdd + MaxNumMulti;
        }
    }
}
