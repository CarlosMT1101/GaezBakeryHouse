using Microsoft.Extensions.Logging;
using MauiIcons.Fluent;
using MauiIcons.Material;
using CommunityToolkit.Maui;
using GaezBakeryHouse.App.ViewModels;
using GaezBakeryHouse.App.Views;

namespace GaezBakeryHouse.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseFluentMauiIcons()
                .UseMaterialMauiIcons()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("typicons.ttf", "typicons");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}