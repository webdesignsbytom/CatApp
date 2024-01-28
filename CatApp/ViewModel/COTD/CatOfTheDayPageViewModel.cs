using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace CatApp.ViewModel.COTD
{
    public partial class CatOfTheDayPageViewModel : ObservableObject
    {
        private IAudioPlayer audioPlayer;

        [ObservableProperty]
        public string currentVideoUrl = "embed://Test.mp4";

        public CatOfTheDayPageViewModel()
        {
            StartAudioPlayback();
        }

        public async void StartAudioPlayback()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("cats_audio3.mp3"));
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
