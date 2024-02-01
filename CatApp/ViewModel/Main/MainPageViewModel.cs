using Aptabase.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CatApp.ViewModel.Main
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Analytics
        IAptabaseClient _aptabase;

        // Review modal
        [ObservableProperty]
        private bool reviewNotificationVisible = true;

        public MainPageViewModel(IAptabaseClient aptabase)
        {
            _aptabase = aptabase;

            TrackPageLoad();
        }

        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Main" }
                });
        }

        // Toggle controls
        // Close play
        [RelayCommand]
        public async Task CloseReviewModal()
        {
            ReviewNotificationVisible = false;
        }

        // Navigation

        [RelayCommand]
        public async Task NavigateToCotd()
        {
            await Shell.Current.GoToAsync("///CatOfTheDayPage");
        }

        [RelayCommand]
        public async Task NavigateToEndlessCats()
        {
            await Shell.Current.GoToAsync("///EndlessCatsPage");
        }

        [RelayCommand]
        public async Task NavigateToTherapyMode()
        {
            await Shell.Current.GoToAsync("///TherapyModePage");
        }

        [RelayCommand]
        public async Task NavigateToMenuPage()
        {
            await Shell.Current.GoToAsync("///MenuMainPage");
        }
    }
}
