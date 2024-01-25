 using CatApp.View.COTD;
using CatApp.View.Endless;
using CatApp.View.Games;
using CatApp.View.Main;
using CatApp.View.TherapyMode;
using CatApp.ViewModel.COTD;
using CatApp.ViewModel.Endless;
using CatApp.ViewModel.Games;
using CatApp.ViewModel.Main;
using CatApp.ViewModel.TherapyMode;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CatApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SegoeIcons.ttf", "Segoe Fluent Icons");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<CatOfTheDayPage>();
            builder.Services.AddSingleton<CatOfTheDayPageViewModel>();
            builder.Services.AddSingleton<EndlessCatsPage>();
            builder.Services.AddSingleton<EndlessCatsPageViewModel>();
            builder.Services.AddSingleton<TherapyModePage>();
            builder.Services.AddSingleton<TherapyModeViewModel>();
            builder.Services.AddSingleton<GamesPage>();
            builder.Services.AddSingleton<GamesPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
