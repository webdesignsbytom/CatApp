using Aptabase.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CatApp.ViewModel.Endless
{
    public partial class EndlessCatsPageViewModel : ObservableObject
    {
        // Audio
        private IAudioPlayer audioPlayer;
        // Analytics
        IAptabaseClient _aptabase;
        // Media 
        public MediaElement MediaElementController { get; set; }


        public EndlessCatsPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
            CreateVideoPlaylist();
        }

        // Video controls
        private List<string> videoFiles;
        private int currentIndex;
        private string currentVideoPath;

        public event PropertyChangedEventHandler PropertyChanged;

        public void CreateVideoPlaylist()
        {
            videoFiles = new List<string>
        {
            "cat_video1.mp4",
            "cat_video2.mp4",
            "cat_video3.mp4",
            "cat_video4.mp4",
            "cat_video5.mp4",
            "cat_video6.mp4",
            "cat_video7.mp4",
            "cat_video8.mp4",
            "cat_video9.mp4",
            "cat_video10.mp4",
            "cat_video11.mp4",
            "cat_video12.mp4",
        };
            currentIndex = 0; // Start with the first video
            UpdateCurrentVideoPath();
        }
        public async void SetFirstVideoSource(MediaElement mediaElement)
        {
            MediaElementController = mediaElement;
            MediaElementController.Source = MediaSource.FromResource("cat_video7.mp4");
        }

        public string CurrentVideoPath
        {
            get => currentVideoPath;
            set
            {
                if (currentVideoPath != value)
                {
                    currentVideoPath = value;
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateCurrentVideoPath()
        {
            CurrentVideoPath = videoFiles[currentIndex]; // Update the path to the current video
                                                         // Here, you can also add code to actually load and play the video
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Audio controls
        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("cats_audio1.mp3"));
            audioPlayer.Volume = 0.5;
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

        // Analytics
        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Endless Cats" }
                });
        }

        // Like video
        [RelayCommand]
        public async Task LikeVideo()
        {
            return;
        }

        // Previous video
        [RelayCommand]
        public async Task OpenPreviousVideo()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateCurrentVideoPath();
                MediaElementController.Source = MediaSource.FromResource($"{CurrentVideoPath}");
            }
        }

        // Next video
        [RelayCommand]
        public async Task OpenNextVideo()
        {
            if (currentIndex < videoFiles.Count - 1)
            {
                currentIndex++;
                UpdateCurrentVideoPath();
                MediaElementController.Source = MediaSource.FromResource($"{CurrentVideoPath}");
            }
        }

        // Home
        [RelayCommand]
        public async Task NavigateBackToMain()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
