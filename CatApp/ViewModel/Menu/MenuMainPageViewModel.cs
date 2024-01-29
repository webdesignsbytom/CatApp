using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.Menu
{
    public partial class MenuMainPageViewModel : ObservableObject
    {
        
        [RelayCommand]
        public async Task NavigateToAiCatsPage()
        {
            await Shell.Current.GoToAsync("///AiCatsPage");
        }           
        
        [RelayCommand]
        public async Task NavigateToGamePage()
        {
            await Shell.Current.GoToAsync("///Games Page");
        }        
        
        [RelayCommand]
        public async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
