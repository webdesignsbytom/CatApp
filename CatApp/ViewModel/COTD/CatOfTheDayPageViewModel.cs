using Aptabase.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CatApp.ViewModel.COTD
{
    public partial class CatOfTheDayPageViewModel : ObservableObject
    {
        // Audio
        private IAudioPlayer audioPlayer;
        // Analytics
        IAptabaseClient _aptabase;
        // Media 
        public MediaElement MediaElementController { get; set; }

        // Swipe modal visible
        [ObservableProperty]
        public bool swipeModalIsVisiable = true;    
        // Control buttons visible
        [ObservableProperty]
        public bool controlButtonsAreVisible = true;

        public CatOfTheDayPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
            CreateVideoPlaylist();
        }

        // Timer
        public async void RemoveSwipeModal()
        {
            SwipeModalIsVisiable = false;
        }        
        // Hide buttons
        public async void HideControlButtons()
        {
            ControlButtonsAreVisible = false;
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
            "Video/cat_video1.mp4",
            "Video/cat_video2.mp4",
            "Video/cat_video3.mp4",
            "Video/cat_video4.mp4",
            "Video/cat_video5.mp4",
            "Video/cat_video6.mp4",
            "Video/cat_video7.mp4",
            "Video/cat_video8.mp4",
            "Video/cat_video9.mp4",
            "Video/cat_video_sponsor.mp4",
            "Video/cat_video10.mp4",
            "Video/cat_video11.mp4",
            "Video/cat_video12.mp4",
        };
            currentIndex = 0; // Start with the first video
            UpdateCurrentVideoPath();
        }
        public async void SetFirstVideoSource(MediaElement mediaElement)
        {
            MediaElementController = mediaElement;
            MediaElementController.Source = MediaSource.FromResource("Video/cat_video1.mp4");
            // MediaElementController.Source = "http://localhost:4000/videos/cat-of-the-day";
            // MediaElementController.Source = "https://cat-app-server-sigma.vercel.app/videos/cat-of-the-day";
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
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Audio/cotd_audio1.mp3"));
            audioPlayer.Volume = 0.5;
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

        // Analytics
        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Cat of the Day" }
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
