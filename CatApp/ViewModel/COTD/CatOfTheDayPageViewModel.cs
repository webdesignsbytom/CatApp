using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.COTD
{
    public partial class CatOfTheDayPageViewModel : ObservableObject
    {
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
