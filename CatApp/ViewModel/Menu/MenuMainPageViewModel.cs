using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.Menu
{
    public partial class MenuMainPageViewModel : ObservableObject
    {
        
        [RelayCommand]
        public async Task NavigateToAiCatsPage()
        {
            await Shell.Current.GoToAsync("///AiImagesPage");
        }           
        
        [RelayCommand]
        public async Task NavigateToGamePage()
        {
            await Shell.Current.GoToAsync("///GamesPage");
        }         
        
        [RelayCommand]
        public async Task VisitSponsorWebsite()
        {
            // Logic to open myecoapp.org page
            string url = "https://www.myecoapp.org/";
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }        
        
        [RelayCommand]
        public async Task NavigateToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
