using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator
{
    //public enum CartItemType
    //{
    //    numberAdd,
    //    numberMulti,
    //    operation,
    //    complexityAdd,
    //    complexityMulti,
    //    computingObject
    //}

    //public class CartItem
    //{
    //    public CartItemType Type { get; set; }
    //    public int? MaxNumAdd { get; set; }
    //    public int? MaxNumMulti { get; set; }
    //    public OperationCondition? OperationCondition { get; set; }
    //    public ComplexityConditionAdd? ComplexityConditionAdd { get; set; }
    //    public ComplexityConditionMulti? ComplexityConditionMulti { get; set; }
    //    public ComputingObject ComputingObject { get; set; }
    //    public int Price { get; set; }

    //    public override string ToString()
    //    {
    //        switch (Type)
    //        {
    //            case CartItemType.numberAdd:
    //                return "Číslo sčítání: " + MaxNumAdd + " (" + Price + ")";
    //            case CartItemType.numberMulti:
    //                return "Číslo násobení: " + MaxNumMulti + " (" + Price + ")";
    //            case CartItemType.operation:
    //                return "Operace: " + Generator.OperationToString((OperationCondition)OperationCondition) + " (" + Price + ")";
    //            case CartItemType.complexityAdd:
    //                return "Podmínka: " + Generator.ComplexityConditionAddToString((ComplexityConditionAdd)ComplexityConditionAdd) + " (" + Price + ")";
    //            case CartItemType.complexityMulti:
    //                return "Podmínka: " + Generator.ComplexityConditionMultiToString((ComplexityConditionMulti)ComplexityConditionMulti) + " (" + Price + ")";
    //            case CartItemType.computingObject:
    //                return "Objekt s komplexitou: " + ComputingObject.Complexity + " (" + Price + ")";
    //            default:
    //                throw new Exception("Unknown CartItemType");
    //        }
    //    }
    //}
}
