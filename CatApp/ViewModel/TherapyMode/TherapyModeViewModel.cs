using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.TherapyMode
{
    public partial class TherapyModeViewModel : ObservableObject
    {
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
