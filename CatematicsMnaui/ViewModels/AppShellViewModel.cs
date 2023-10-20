using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatematicsMnaui.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        public AppShellViewModel(EquationPageViewModel equationPageViewModel, MyCatsPageViewModel myCatsPageViewModel, ShopPageViewModel shopPageViewModel)
        {
            EquationPageViewModel = equationPageViewModel;
            MyCatsPageViewModel = myCatsPageViewModel;
            ShopPageViewModel = shopPageViewModel;
        }

        [ObservableProperty]
        private EquationPageViewModel _equationPageViewModel;

        [ObservableProperty]
        private MyCatsPageViewModel _myCatsPageViewModel;

        [ObservableProperty]
        private ShopPageViewModel _shopPageViewModel;
    }
}
