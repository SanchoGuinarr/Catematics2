using EquationGenerator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catematics.ViewModel
{
    internal class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<CartItemViewModel> CartItems { get; set; } = new();
        public ObservableCollection<ComputingObjectViewModel> ComputingObjects { get; set; } = new();
        public EquationViewModel Equation { get; set; }

        public string Money => Sequence.Treasure.Money.ToString();

        private EquationSequence Sequence;

        public MainWindowViewModel()
        {
            Sequence = new EquationSequence();
            NewEquation();
        }

        private void NewEquation()
        {
            var equation = Sequence.NextEquation();
            Equation = new(equation);
            Equation.SuccessfullyComputed += Equation_SuccessfullyComputed;
            OnPropertyChange(nameof(Equation));
        }

        private void Equation_SuccessfullyComputed(object sender, AEquation equation)
        {
            Sequence.EquationSolved(equation);
            OnPropertyChange(nameof(Money));
            UpdateCart();
        }
        private void UpdateCart()
        {
            CartItems = new();
            foreach(var item in Sequence.Cart)
            {
                var cartItem = new CartItemViewModel(item, Sequence.Treasure);
                cartItem.ItemBought += CartItem_ItemBought;
                CartItems.Add(cartItem);
            }
            OnPropertyChange(nameof(CartItems));
        }

        private void CartItem_ItemBought(object sender, EquationGenerator.Interfaces.ICartItem item)
        {
            Sequence.Buy(item);
            UpdateCart();
            if(item is ComputingObject computingObject)
            {
                ComputingObjects.Add(new ComputingObjectViewModel(computingObject));
            }
            OnPropertyChange(nameof(ComputingObjects));
        }
    }
}
