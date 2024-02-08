using Aptabase.Maui;
using CatApp.Model.Game;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using SkiaSharp;
using System.Reflection;

namespace CatApp.ViewModel.Games
{
    public partial class GamesPageViewModel : ObservableObject
    {
        // Analytics
        IAptabaseClient _aptabase;
        // Audio
        private IAudioPlayer audioPlayer;

        // Cat models
        public CatModel CatOne = new ("Tippy");
        public CatModel CatTwo = new ("Maze");
        public CatModel CatThree = new ("Raffa");
        public CatModel CatFour = new ("Kiki");

        // Constructor
        public GamesPageViewModel(IAptabaseClient aptabase)
        {
            // Analytics
            _aptabase = aptabase;

            // Analytics
            TrackPageLoad();

            // Set up game
            SetUpGame();
        }

        // Analytics
        private void TrackPageLoad()
        {
            // Sends data back to aptabase
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Games" }
                });
        }

        // Game Functions //

        // Bitmap of cats
        private SKBitmap cat1Bitmap;
        private SKBitmap cat2Bitmap;
        private SKBitmap cat3Bitmap;
        private SKBitmap cat4Bitmap;

        // Canvas
        public SKCanvas? gameCanvas;

        // Selected cat displayed
        public CatSelection SelectedCat { get; set; } = CatSelection.Cat1;


        // Basic Game setup
        public void SetUpGame()
        {
            // Create animations to use in game
            CreateGameBitmapAnimations();
            StartAudioPlayback();
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
            // Image source
            string imageSource = "CatApp.Resources.Images.Games.";

            // Create cat bitmaps
            // Cat 1
            using var cat1BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat1.png");
            cat1Bitmap = SKBitmap.Decode(cat1BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
            // Cat 2
            using var cat2BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat2.png");
            cat2Bitmap = SKBitmap.Decode(cat2BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
            // Cat 3
            using var cat3BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat3.png");
            cat3Bitmap = SKBitmap.Decode(cat3BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
            // Cat 4
            using var cat4BitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{imageSource}cat4.png");
            cat4Bitmap = SKBitmap.Decode(cat4BitmapStream).Resize(new SKImageInfo(300, 600), SKFilterQuality.Low);
        }

        // Draw Game
        public void DrawGameAnimations()
        {
            // Check null
            if (gameCanvas == null)
            {
                throw new InvalidOperationException("gameCanvas must be set to an instance of an object");
            }

            // Scale
            var mat = SKMatrix.CreateScale(1.0f, 1.0f);

            // Selected cat image
            var imageSelected = cat1Bitmap;

            switch (SelectedCat)
            {
                case CatSelection.Cat1:
                    imageSelected = cat1Bitmap;
                    break;
                case CatSelection.Cat2:
                    imageSelected = cat2Bitmap;
                    break;
                case CatSelection.Cat3:
                    imageSelected = cat3Bitmap;
                    break;
                case CatSelection.Cat4:
                    imageSelected = cat4Bitmap;
                    break;
            }


            // Set postion and draw on screen
            var catPos = mat.Invert().MapPoint((float)65.0f, (float)120.0f);
            gameCanvas.DrawBitmap(imageSelected, new SKPoint(catPos.X, catPos.Y), new SKPaint());

            // Create border for interactions
            var playerRect = mat.Invert().MapRect(new SKRect(catPos.X - 15, catPos.Y, catPos.X + imageSelected.Width, catPos.Y + imageSelected.Height));

            // Draw border
            gameCanvas.DrawRect(playerRect, new SKPaint()
            {
                IsStroke = true,
                Color = SKColors.Red
            });
        }


        // Choose next cat 
        [RelayCommand]
        public async Task SwitchToNextCat()
        {
            // Cycle through the CatSelection enum
            SelectedCat = SelectedCat switch
            {
                CatSelection.Cat1 => CatSelection.Cat2,
                CatSelection.Cat2 => CatSelection.Cat3,
                CatSelection.Cat3 => CatSelection.Cat4,
                CatSelection.Cat4 => CatSelection.Cat1,
                _ => SelectedCat
            };
        }

        // Cats
        public enum CatSelection
        {
            Cat1,
            Cat2,
            Cat3,
            Cat4
        }

        // Audio controls
        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Audio/Game/purring_1.mp3"));
            audioPlayer.Volume = 0.05;
            audioPlayer.Loop = true;
            audioPlayer.Play();
        }

        public void StopAudioPlayback()
        {
            audioPlayer?.Stop();
        }

        public void PauseAudioPlayback()
        {
            audioPlayer?.Pause();
        }

        public void PlayAudioPlayback()
        {
            audioPlayer?.Play();
        }


        [RelayCommand]
        public async Task Volume()
        {
            audioPlayer.Volume = 1;
        }

        // Home navigation
        [RelayCommand]
        public async Task NavigateToMenuPage()
        {
            await Shell.Current.GoToAsync("///MenuMainPage");
        }
    }
}
