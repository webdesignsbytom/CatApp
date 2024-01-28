using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace CatApp.ViewModel.TherapyMode
{
    public partial class TherapyModeViewModel : ObservableObject
    {
        private IAudioPlayer audioPlayer;

        public TherapyModeViewModel()
        {
            StartAudioPlayback();
        }

        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("cats_audio2.mp3"));
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

        // Home
        [RelayCommand]
        public async Task NavigateBackToMain()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
