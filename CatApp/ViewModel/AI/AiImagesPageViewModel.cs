using Aptabase.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;

namespace CatApp.ViewModel.AI
{
    public partial class AiImagesPageViewModel : ObservableObject
    {
        private IAudioPlayer audioPlayer;
        private int currentImageIndex = 0;
        private ObservableCollection<string> imagePaths = new ObservableCollection<string>();

        IAptabaseClient _aptabase;

        public AiImagesPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
            LoadImagePaths();

        }

        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "AI Cats" }
                });
        }


        private void LoadImagePaths()
        {
/*            var imageFolder = Path.Combine(FileSystem.AppPackageDirectory, "Resources", "Images", "Ai");
            var files = Directory.GetFiles(imageFolder);
            foreach (var file in files)
            {
                imagePaths.Add(file);
            }*/
        }

        public string CurrentImagePath => imagePaths.Count > 0 ? imagePaths[currentImageIndex] : null;

        [RelayCommand]
        public void PlayNextImage()
        {
            if (imagePaths.Count > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % imagePaths.Count;
                OnPropertyChanged(nameof(CurrentImagePath));
            }
        }
    }
}
