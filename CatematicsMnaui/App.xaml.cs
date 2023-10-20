using CatematicsMnaui.ViewModels;
using CatematicsMnaui.Views;

namespace CatematicsMnaui
{
    public partial class App : Application
    {
        public App(AppShellViewModel appShellViewModel)
        {
            InitializeComponent();

            MainPage = new AppShell(appShellViewModel);
        }
    }
}