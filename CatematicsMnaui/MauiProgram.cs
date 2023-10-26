using CatematicsMnaui.ViewModels;
using EquationGenerator;
using EquationGenerator.Services;
using EquationGenerator.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CatematicsMnaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterAppServices()
                .RegisterViewModels()
                ;

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder builder)
        {
            // builder.Services.AddSingleton<IEquationSequenceService, EquationSequenceService>();
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddSingleton<IComplexityStateService, ComplexityStateService>();
            builder.Services.AddSingleton<IGeneratorService, GeneratorService>();
            builder.Services.AddSingleton<ISettingsService, SettingsService>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<EquationPageViewModel>();
            builder.Services.AddTransient<MyCatsPageViewModel>();
            builder.Services.AddTransient<ShopPageViewModel>();
            builder.Services.AddTransient<EquationViewModel>();
            return builder;
        }
    }
}