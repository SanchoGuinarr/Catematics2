using CommunityToolkit.Mvvm.Messaging.Messages;
using EquationGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.Models.Messages
{
    public class StateChangedMessage: ValueChangedMessage<State>
    {
        public StateChangedMessage(State state) : base(state)
        {
        }
    }
}
