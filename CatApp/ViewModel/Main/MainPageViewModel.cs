using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.Main
{
    public partial class MainPageViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task NavigateToCotd()
        {
            await Shell.Current.GoToAsync("///CatOfTheDayPage");
        }

        [RelayCommand]
        public async Task NavigateToEndlessCats()
        {
            await Shell.Current.GoToAsync("///CatOfTheDayPage");
        }

        [RelayCommand]
        public async Task NavigateToTherapyMode()
        {
            await Shell.Current.GoToAsync("///CatOfTheDayPage");
        }

        [RelayCommand]
        public async Task NavigateToGmesPage()
        {
            await Shell.Current.GoToAsync("///CatOfTheDayPage");
        }
    }
}
