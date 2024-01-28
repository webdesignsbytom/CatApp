using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace CatApp.ViewModel.Endless
{
    public partial class EndlessCatsPageViewModel : ObservableObject 
    {
        private IAudioPlayer audioPlayer;

        public EndlessCatsPageViewModel()
        {
            StartAudioPlayback();
        }

        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("cats_audio1.mp3"));
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

        // Next video
        [RelayCommand]
        public async Task PlayNextVideo()
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
