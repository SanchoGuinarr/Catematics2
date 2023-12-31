﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using EquationGenerator;
using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.Models.Messages
{
    public class ItemPurchasedMessage : ValueChangedMessage<IComputingObject>
    {
        public ItemPurchasedMessage(IComputingObject computingObject) : base(computingObject)
        {
        }
    }
}
