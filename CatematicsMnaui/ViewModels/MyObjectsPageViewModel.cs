using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator.Interfaces;
using EquationGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public class MyObjectsPageViewModel : ObservableObject, IRecipient<ItemPurchasedMessage>
    {
        private readonly IComputingObjectService _computingObjectService;

        public List<ComputingObjectViewModel> ComputingObjects { get; set; } = new();

        public MyObjectsPageViewModel(IComputingObjectService computingObjectService)
        {
            _computingObjectService = computingObjectService;

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(ItemPurchasedMessage message)
        {
            ComputingObjects = _computingObjectService.ComputingObjects.Select(x => new ComputingObjectViewModel(x)).ToList();
            OnPropertyChanged(nameof(ComputingObjects));
        }
    }
}
