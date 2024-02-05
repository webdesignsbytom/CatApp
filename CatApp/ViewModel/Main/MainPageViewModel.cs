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
        private bool reviewNotificationVisible = false;

        // Reviews
        public int ReviewVisitCount { get; set; } = 0;

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
        
        [RelayCommand]
        public async Task OpenReviewModal()
        {
            ReviewNotificationVisible = true;
        }

        // Review int
        public void IncreaseReviewInt()
        {
            ReviewVisitCount++;
        }        
        
        public void ResetReviewInt()
        {
            ReviewVisitCount = 0;
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
