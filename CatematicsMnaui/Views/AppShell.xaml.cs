using CatematicsMnaui.ViewModels;

namespace CatematicsMnaui.Views;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();

		BindingContext = appShellViewModel;
	}
}