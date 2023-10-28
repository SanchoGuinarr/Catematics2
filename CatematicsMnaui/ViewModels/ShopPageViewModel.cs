using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator;
using EquationGenerator.Services;
using EquationGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class ShopPageViewModel : ObservableObject, IRecipient<MoneyChangedMessage>
    {
        private readonly IComplexityStateService _complexityStateService;
        private readonly ICartService _cartService;
        
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        public ShopPageViewModel(IComplexityStateService complexityStateService, ICartService cartService)
        {
            _complexityStateService = complexityStateService;
            _cartService = cartService;

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(MoneyChangedMessage message)
        {
            RefreshCartItems(message.Value);
        }

        private void RefreshCartItems(MoneyCounter moneyCounter)
        {
            CartItems = _cartService.CartItems.Select(x => new CartItemViewModel(x, moneyCounter)).ToList();
            OnPropertyChanged(nameof(CartItems));
        }
    }

}
