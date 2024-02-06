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
        private SKBitmap cat1Bitmap;
        private SKBitmap cat2Bitmap;
        private SKBitmap cat3Bitmap;
        private SKBitmap cat4Bitmap;
        // Canvas
        public SKCanvas? gameCanvas;
        // Cat image
        public int SelectedCatInt { get; set; } = 0;
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
            using var cat1BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat1.png");
            cat1Bitmap = SKBitmap.Decode(cat1BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
            using var cat2BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat2.png");
            cat2Bitmap = SKBitmap.Decode(cat2BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
            using var cat3BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat3.png");
            cat3Bitmap = SKBitmap.Decode(cat3BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
            using var cat4BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat4.png");
            cat4Bitmap = SKBitmap.Decode(cat4BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
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
            var imageSizeSelected = cat1Bitmap;

            switch (SelectedCatInt)
            {
                case 0:
                    imageSizeSelected = cat1Bitmap;
                    break;
                case 1:
                    imageSizeSelected = cat2Bitmap;
                    break;
                case 2:
                    imageSizeSelected = cat3Bitmap;
                    break;
                case 3:
                    imageSizeSelected = cat4Bitmap;
                    break;
            }


            var catPos = mat.Invert().MapPoint((float)65.0f, (float)120.0f);
            gameCanvas.DrawBitmap(imageSizeSelected, new SKPoint(catPos.X, catPos.Y), new SKPaint());
        }

        // Choose next cat 
        [RelayCommand]
        public async Task SwitchToNextCat()
        {
            if (SelectedCatInt == 3)
            {
                SelectedCatInt = 0;
            }
            else
            {
                SelectedCatInt++;
            }
        }

        // Home navigation
        [RelayCommand]
        public async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
