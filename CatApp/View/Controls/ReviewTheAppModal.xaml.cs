namespace CatApp.View.Controls;

public partial class ReviewTheAppModal : ContentView
{
	public ReviewTheAppModal()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        OpenAppStoreAsync();
    }

    public async Task OpenAppStoreAsync()
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
            await Launcher.OpenAsync(storeUrl);
        }
    }

}