using CommunityToolkit.Mvvm.Messaging.Messages;
using EquationGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.Models.Messages
{
    public class StartNewSequenceMessage: ValueChangedMessage<SettingsDefinition>
    {
        public StartNewSequenceMessage(SettingsDefinition settings) : base(settings)
        {
        }
    }
}
