using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.Endless
{
    public partial class EndlessCatsPageViewModel : ObservableObject 
    {
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
