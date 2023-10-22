using CommunityToolkit.Mvvm.Messaging.Messages;
using EquationGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.Models.Messages
{
    public class NumberInsertedMessage: ValueChangedMessage<string>
    {
        public NumberInsertedMessage(string number) : base(number)
        {
        }
    }
}
