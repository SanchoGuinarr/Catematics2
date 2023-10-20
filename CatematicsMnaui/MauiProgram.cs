using CatematicsMnaui.ViewModels;
using EquationGenerator;
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
            builder.Services.AddSingleton<IEquationSequenceService, EquationSequenceService>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<EquationPageViewModel>();
            builder.Services.AddTransient<MyCatsPageViewModel>();
            builder.Services.AddTransient<ShopPageViewModel>();
            return builder;
        }
    }
}