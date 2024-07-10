using CatApp.Model.User;
using CatApp.Services.Utils;
using CatApp.View.AI;
using CatApp.View.COTD;
using CatApp.View.Endless;
using CatApp.View.Games;
using CatApp.View.Main;
using CatApp.View.Menu;
using CatApp.View.TherapyMode;
using CatApp.ViewModel.AI;
using CatApp.ViewModel.COTD;
using CatApp.ViewModel.Endless;
using CatApp.ViewModel.Games;
using CatApp.ViewModel.Main;
using CatApp.ViewModel.Menu;
using CatApp.ViewModel.TherapyMode;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace CatApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseAptabase("A-EU-1714048004") // 👈 this is where you enter your App Key
                .UseMauiCommunityToolkitMediaElement() // Video player
                .UseSkiaSharp() // Canvas
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SegoeIcons.ttf", "Segoe Fluent Icons"); // Icons 
                });

            // Pages
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
            builder.Services.AddSingleton<MenuMainPage>();
            builder.Services.AddSingleton<MenuMainPageViewModel>();
            builder.Services.AddSingleton<AiImagesPage>();
            builder.Services.AddSingleton<AiImagesPageViewModel>();
            // Models
            builder.Services.AddSingleton<UserModel>();
            // Register HttpClient
            builder.Services.AddHttpClient<HttpService>();

            // Register services
            builder.Services.AddSingleton<VideoService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
