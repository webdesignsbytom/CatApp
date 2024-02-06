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
        public async Task ShareTheCatApp()
        {
            Console.WriteLine("1111111111111111111111111111111111");
            OpenShareStore();
        }

        public async Task OpenShareStore()
        {
            Console.WriteLine("222222222222222222222222222222");
            // Share code
            string storeUrl = string.Empty;

            // Perform platform check at runtime
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                // iOS App Store URL
                storeUrl = "https://apps.apple.com/app/idYOUR_APP_ID";
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Console.WriteLine("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");
                // Android Google Play Store URL
                storeUrl = "https://play.google.com/store/apps/details?id=YOUR_PACKAGE_NAME";
            }

            // Check if the URL can be opened and then open it
            if (!string.IsNullOrEmpty(storeUrl) && await Launcher.CanOpenAsync(storeUrl))
            {
                Console.WriteLine("CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC");
                await Launcher.OpenAsync(storeUrl);
            }
        }


        [RelayCommand]
        public async Task VisitSponsorWebsite()
        {
            // Open sponsor
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
