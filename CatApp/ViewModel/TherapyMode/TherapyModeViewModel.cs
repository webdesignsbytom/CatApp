using Aptabase.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace CatApp.ViewModel.TherapyMode
{
    public partial class TherapyModeViewModel : ObservableObject
    {
        // Audio
        private IAudioPlayer audioPlayer;
        // Analytics
        IAptabaseClient _aptabase;

        // Control buttons visible
        [ObservableProperty]
        public bool controlButtonsAreVisible = true;
        // Prevent double taps
        public bool HasTappedScreen = true;

        // Media
        public MediaElement MediaElementController { get; set; }
        public TherapyModeViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
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
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            ShowControlButtons();
        }


        // Video Controls
        public async void SetVideoSource(MediaElement mediaElement)
        {
            MediaElementController = mediaElement;
            MediaElementController.Source = MediaSource.FromResource("Video/cat_video1.mp4");
        }

        // Analytics
        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Therapy" }
                });
        }
        
        // Audio controls
        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Audio/cat_audio2.mp3"));
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

        // Like video
        [RelayCommand]
        public async Task LikeVideo()
        {
            return;
        }

        // Home
        [RelayCommand]
        public async Task NavigateBackToMain()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
