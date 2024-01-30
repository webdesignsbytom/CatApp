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
        // Media 
        public MediaElement MediaElementController { get; set; }
        public TherapyModeViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
        }

        // Video Controls
        public async void SetVideoSource(MediaElement mediaElement)
        {
            MediaElementController = mediaElement;
            MediaElementController.Source = MediaSource.FromResource("cat_video3.mp4");
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
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("cats_audio2.mp3"));
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
            audioPlayer?.Pause();
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
