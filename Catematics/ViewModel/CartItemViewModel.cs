using EquationGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catematics.ViewModel
{
    class CartItemViewModel
    {
        public event EventHandler<ICartItem> ItemBought;

        public ICommand ButtonBuyCommand { get; set; }
        public string Name => model.Name;
        public string Price => model.Price.ToString();

        private ICartItem model;
        private IMoneyCounter money;
        public CartItemViewModel(ICartItem item, IMoneyCounter moneyCounter)
        {
            model = item;
            money = moneyCounter;
            ButtonBuyCommand = new RelayCommand<object>(new Action<object>(Buy), new Predicate<object>(CanBuy));
        }

        private void Buy(object obj)
        {
            ItemBought?.Invoke(this, model);
        }

        private bool CanBuy(object obj)
        {
            return money.Money > model.Price;
        }
    }
}
