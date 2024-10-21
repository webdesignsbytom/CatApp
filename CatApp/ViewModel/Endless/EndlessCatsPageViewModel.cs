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

        // Swipe modal visible
        [ObservableProperty]
        public bool swipeModalIsVisiable = true;
        // Control buttons visible
        [ObservableProperty]
        public bool controlButtonsAreVisible = true;
        // Prevent double taps
        public bool HasTappedScreen = true;

        public EndlessCatsPageViewModel(IAptabaseClient aptabase)
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

        public async void ShowControlButtons()
        {
            ControlButtonsAreVisible = true;
        }

        public async Task OnScreenTap()
        {
            Console.WriteLine("Screen tapped");
            ShowControlButtons();
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
                "http://192.168.1.150:9000/catapp/videos/cotd/1718828165218.mp4",
                "http://192.168.1.150:9000/catapp/videos/cotd/1718828165218.mp4",
                "http://192.168.1.150:9000/catapp/videos/cotd/1718828165218.mp4",
                "http://192.168.1.150:9000/catapp/videos/cotd/1718828165218.mp4",
                "http://192.168.1.150:9000/catapp/videos/cotd/1718828165218.mp4",
                "http://192.168.1.150:9000/catapp/videos/cotd/1718828165218.mp4",
            };

            // Shuffle the videoFiles list
            Random rng = new Random();
            videoFiles = videoFiles.OrderBy(a => rng.Next()).ToList();

            currentIndex = 0; // Start with the first video
            UpdateCurrentVideoPath();
        }

        public async void SetFirstVideoSource(MediaElement mediaElement)
        {
            MediaElementController = mediaElement;
            // Use FromUri for remote URLs
            MediaElementController.Source = MediaSource.FromUri(currentVideoPath);
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

        [ObservableProperty]
        public bool isMuted = false;
        [ObservableProperty]
        public bool isNotMuted = true;

        // Mute audio
        [RelayCommand]
        public async Task MuteAudio()
        {
            if (!IsMuted)
            {
                audioPlayer.Pause();
                IsNotMuted = false;
                IsMuted = true;
            }
            else
            {
                audioPlayer.Play();
                IsMuted = false;
                IsNotMuted = true;
            }
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
            // Implement functionality for liking the video if needed
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
                MediaElementController.Source = MediaSource.FromUri($"{CurrentVideoPath}");
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
                MediaElementController.Source = MediaSource.FromUri($"{CurrentVideoPath}");
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
