using Aptabase.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CatApp.ViewModel.AI
{
    public partial class AiImagesPageViewModel : ObservableObject
    {
        // Audio
        private IAudioPlayer audioPlayer;
        // Analytics
        IAptabaseClient _aptabase;
        // Images
        public int currentImageIndex = 1;
        private int TotalImagesNum = 47;

        [ObservableProperty]
        public string currentImage = "Resources/Images/Ai/cat_ai_image_1.png";

        public AiImagesPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
            UpdateCurrentImage(); // Call this to ensure the image is set correctly at startup
        }

        // Next image
        [RelayCommand]
        public void OpenNextImage()
        {
            if (currentImageIndex <= TotalImagesNum)
            {
                currentImageIndex++;
                UpdateCurrentImage();
            }
        }

        // Previous image
        [RelayCommand]
        public void OpenPreviousImage()
        {
            if (currentImageIndex > 1)
            {
                currentImageIndex--;
                UpdateCurrentImage();
            }
        }

        // Update current image based on index
        private void UpdateCurrentImage()
        {
            currentImage = $"Resources/Images/Ai/cat_ai_image_{currentImageIndex}.png";
            OnPropertyChanged(nameof(CurrentImage)); // Notify the UI to update the image
        }

        // Analytics
        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "AI Cats" }
            });
        }

        // Navigate home
        [RelayCommand]
        public async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
