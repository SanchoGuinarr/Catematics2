using CommunityToolkit.Mvvm.Messaging.Messages;
using EquationGenerator;
using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.Models.Messages
{
    public class BuyClickedMessage : ValueChangedMessage<ICartItem>
    {
        public BuyClickedMessage(ICartItem cartItem) : base(cartItem)
        {
        }
    }
}
