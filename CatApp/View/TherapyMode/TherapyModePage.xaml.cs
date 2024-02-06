using CatApp.ViewModel.TherapyMode;
using CommunityToolkit.Maui.Views;

namespace CatApp.View.TherapyMode;

public partial class TherapyModePage : ContentPage
{
	public TherapyModeViewModel ViewModel { get; set; }
	public TherapyModePage(TherapyModeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel; 
    }

    protected override void OnAppearing()
    {
        if (BindingContext is TherapyModeViewModel viewModel)
        {
            viewModel.StartAudioPlayback();
        }

        var mediaElement = this.FindByName<MediaElement>("TherapyModeMediaPlayer");
        if (mediaElement != null)
        {
            SetFirstVideo(mediaElement);
            mediaElement.Play();
        }

        PostAppearanceActions();
    }

    // Show menu buttons and label
    public void ShowTempComponents()
    {
        ViewModel.ShowControlButtons();
    }

    // Hide menu and swipe after 5 seconds
    private async void PostAppearanceActions()
    {
        // Wait for 5 seconds
        await Task.Delay(5000);

        ViewModel.HideControlButtons();
        ViewModel.HasTappedScreen = false;
    }

    public async Task SetFirstVideo(MediaElement mediaElement)
    {
        if (BindingContext is TherapyModeViewModel viewModel)
        {
            viewModel.SetVideoSource(mediaElement);
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is TherapyModeViewModel viewModel)
        {
            viewModel.StopAudioPlayback();
        }

        var mediaElement = this.FindByName<MediaElement>("TherapyModeMediaPlayer");
        if (mediaElement != null)
        {
            mediaElement.Stop();
        }
    }

    // Reopen control buttons
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (ViewModel.HasTappedScreen == true)
        {
            return;
        }
        else
        {
            ViewModel.HasTappedScreen = true;
            ViewModel.OnScreenTap();
            CloseControlButtons();
        }
    }

    // Hide menu and swipe after 5 seconds
    private async void CloseControlButtons()
    {
        // Wait for 5 seconds
        await Task.Delay(5000);

        ViewModel.HideControlButtons();
        ViewModel.HasTappedScreen = false;
    }
}