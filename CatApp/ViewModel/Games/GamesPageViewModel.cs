using Aptabase.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;
using System.Reflection;

namespace CatApp.ViewModel.Games
{
    public partial class GamesPageViewModel : ObservableObject
    {
        // Analytics
        IAptabaseClient _aptabase;

        public GamesPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            // Analytics
            TrackPageLoad();

            // Set up game
            SetUpGame();
        }

        // Analytics
        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Games" }
                });
        }

        // Game Functions

        // Bitmap of cat
        private SKBitmap catBitmap;
        // Canvas
        public SKCanvas? gameCanvas;

        // Basic setup
        public void SetUpGame()
        {
            Console.WriteLine("VM22222222222222222222222222222222222222222222222222");
            // Create animations to use in game
            CreateGameBitmapAnimations();
        }

        // Set canvas from codebehind and runs draw animations
        public void GameLoop(SKCanvas canvas)
        {
            gameCanvas = canvas;
            DrawGameAnimations();
        }

        // Create Bitmaps
        public void CreateGameBitmapAnimations()
        {
            Console.WriteLine("VMCreateGameBitmapAnimations333333333333333333333333333333333");
            // Image source
            string imageSource = "CatApp.Resources.Images.Games.";

            // Create varisou image sizes
            using var catBitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat1.png");
            catBitmap = SKBitmap.Decode(catBitmapStream).Resize(new SKImageInfo(200, 300), SKFilterQuality.Low);
        }

        // Draw Game
        public void DrawGameAnimations()
        {
            Console.WriteLine("VMDrawStartingAnimation444444444444444444444444444444444444");

            if (gameCanvas == null)
            {
                throw new InvalidOperationException("gameCanvas must be set to an instance of an object");
            }

            var mat = SKMatrix.CreateScale(1.0f, 1.0f);

            var imageSizeSelected = catBitmap;

            var catPos = mat.Invert().MapPoint((float)110.0f, (float)220.0f);
            gameCanvas.DrawBitmap(imageSizeSelected, new SKPoint(catPos.X, catPos.Y), new SKPaint());
        }

        // Home navigation
        [RelayCommand]
        public async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
