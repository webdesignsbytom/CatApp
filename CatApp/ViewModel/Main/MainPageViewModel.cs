using Aptabase.Maui;
using CatApp.Model.User;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace CatApp.ViewModel.Main
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Analytics
        IAptabaseClient _aptabase;
        // Audio
        private IAudioPlayer audioPlayer;
        // User
        public UserModel User { get; set; }

        // Review modal
        [ObservableProperty]
        private bool reviewNotificationVisible = false;

        // Reviews - count until popup appears
        public int ReviewVisitCount { get; set; } = 0;

        public MainPageViewModel(IAptabaseClient aptabase, UserModel user)
        {
            _aptabase = aptabase;

            User = user;

            TrackPageLoad();
            LoadAudioFiles();
        }

        // Analytics
        private void TrackPageLoad()
        {
            _aptabase.TrackEvent("screen_view", new() {
                { "name", "Main" }
                });
        }

        // Audio
        private async Task LoadAudioFiles()
        {
            audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Audio/meow_sound_effect.mp3"));
        }

        [RelayCommand]
        public async Task ReviewApp()
        {
            string storeUrl = string.Empty;

            // Perform platform check at runtime
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // iOS App Store URL
                storeUrl = "https://apps.apple.com/app/idYOUR_APP_ID";
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Android Google Play Store URL
                storeUrl = "https://play.google.com/store/apps/details?id=YOUR_PACKAGE_NAME";
            }


            // Check if the URL can be opened and then open it
            if (!string.IsNullOrEmpty(storeUrl) && await Launcher.CanOpenAsync(storeUrl))
            {
                // Prevent review pop up from reappearing
                await SecureStorage.Default.SetAsync("has_reviewed", "true");
                User.HasReviewedApp = true;
                await Launcher.OpenAsync(storeUrl);
            }
        }


        // Toggle controls
        // Close review modal
        [RelayCommand]
        public async Task CloseReviewModal()
        {
            ReviewNotificationVisible = false;
        }

        // Open review modal
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

        // Review int reset
        public void ResetReviewInt()
        {
            ReviewVisitCount = 0;
        }

        // Navigation

        [RelayCommand]
        public async Task NavigateToCotd()
        {
            //audioPlayer.Play();
            await Shell.Current.GoToAsync("///CatOfTheDayPage");
        }

        [RelayCommand]
        public async Task NavigateToEndlessCats()
        {
            //audioPlayer.Play();
            await Shell.Current.GoToAsync("///EndlessCatsPage");
        }

        [RelayCommand]
        public async Task NavigateToTherapyMode()
        {
            //audioPlayer.Play();
            await Shell.Current.GoToAsync("///TherapyModePage");
        }

        [RelayCommand]
        public async Task NavigateToMenuPage()
        {
            //audioPlayer.Play();
            await Shell.Current.GoToAsync("///MenuMainPage");
        }
    }
}
