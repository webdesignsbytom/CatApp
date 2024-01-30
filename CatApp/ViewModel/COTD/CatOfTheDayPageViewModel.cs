using Aptabase.Maui;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace CatApp.ViewModel.COTD
{
    public partial class CatOfTheDayPageViewModel : ObservableObject
    {
        private IAudioPlayer audioPlayer;
        IAptabaseClient _aptabase;

        public CatOfTheDayPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
        }

        public async void SetFirstVideoSource(MediaElement mediaElement)
        {
            mediaElement.Source = MediaSource.FromResource("Test.mp4");
        }

        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Cat of the Day" }
                });
        }

        [ObservableProperty]
        public string currentVideoUrl = "file:///Test.mp4";

        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("cats_audio3.mp3"));
            audioPlayer.Volume = 0.5;
            audioPlayer.Play();
        }

        public void StopAudioPlayback()
        {
            audioPlayer?.Stop();
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
            await Shell.Current.GoToAsync("///MainPage");
        }

        // Next video
        [RelayCommand]
        public async Task OpenNextVideo()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        // Home
        [RelayCommand]
        public async Task NavigateBackToMain()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
