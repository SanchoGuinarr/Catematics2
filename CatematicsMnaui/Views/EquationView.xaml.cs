using CatematicsMnaui.Models;
using CatematicsMnaui.ViewModels;

namespace CatematicsMnaui.Views;

public partial class EquationView : ContentView
{
	public EquationView()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is EquationViewModel equationViewModel)
        {
            equationViewModel.DoAnimation -= ViewModel_DoAnimation;
            equationViewModel.DoAnimation += ViewModel_DoAnimation;
        }
    }

    private async void ViewModel_DoAnimation(object sender, AnimationEventArgs e)
    {
        switch (e.AnimationType)
		{
            case AnimationType.Correct:
                await GreenRectangle.FadeTo(1, 1000);
                await GreenRectangle.FadeTo(0, 1000);
                break;
            case AnimationType.Incorrect:
                await RedRectangle.FadeTo(1, 1000);
                await RedRectangle.FadeTo(0, 1000);
                break;
        }
    }
}