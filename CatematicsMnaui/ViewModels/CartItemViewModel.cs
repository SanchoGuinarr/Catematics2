using CatematicsMnaui.Models.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EquationGenerator;
using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class CartItemViewModel : ObservableObject
    {
        private ICartItem _cartItem;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _price;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _image;

        [ObservableProperty]
        private bool _available;

        [RelayCommand]
        public void Buy()
        {
            WeakReferenceMessenger.Default.Send(new ItemPurchasedMessage(_cartItem));
        }

        public CartItemViewModel(ICartItem cartItem, MoneyCounter moneyCounter)
        {
            _cartItem = cartItem;
            Title = _cartItem.Title;
            Price = _cartItem.Price.ToString();
            Description = _cartItem.Description;
            Image = _cartItem.ImageSource;
            Available = moneyCounter.Money >= _cartItem.Price;
        }

        
    }
}
