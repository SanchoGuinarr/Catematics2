﻿using EquationGenerator.Interfaces;
using EquationGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationGenerator.CartItems
{
    class CartItemOperation : ICartItem
    {
        public int Price { get; set; }

        public string Name => "Operace: " + GeneratorService.OperationToString(Operation) + " (" + Price + ")";

        public OperationCondition Operation { get; set; }
    }
}
