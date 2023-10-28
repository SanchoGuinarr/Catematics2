using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class MoneyCounterViewModel : ObservableObject, IRecipient<MoneyChangedMessage>
    {

        public MoneyCounterViewModel()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        [ObservableProperty]
        private string _displayedNumber;

        public void Receive(MoneyChangedMessage message)
        {
            DisplayedNumber = message.Value.Money.ToString();
        }
    }
}
