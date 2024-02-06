using CatApp.ViewModel.Endless;
using CommunityToolkit.Maui.Views;

namespace CatApp.View.Endless;

public partial class EndlessCatsPage : ContentPage
{
	public EndlessCatsPageViewModel ViewModel { get; set; }
    public EndlessCatsPage(EndlessCatsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}

    protected override void OnAppearing()
    {
        if (BindingContext is EndlessCatsPageViewModel viewModel)
        {
            viewModel.StartAudioPlayback();
        }

        var mediaElement = this.FindByName<MediaElement>("EndlessCatsMediaPlayer");
        if (mediaElement != null)
        {
            SetFirstVideo(mediaElement);
            mediaElement.Play();
        }

        ShowTempComponents();
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

        ViewModel.RemoveSwipeModal();
        ViewModel.HideControlButtons();
        ViewModel.HasTappedScreen = false;
    }

    // Set first video to play
    public async Task SetFirstVideo(MediaElement mediaElement)
    {
        if (BindingContext is EndlessCatsPageViewModel viewModel)
        {
            viewModel.SetFirstVideoSource(mediaElement);
        }
    }

    // Play next video
    private void OnMediaEnded(object sender, EventArgs e)
    {
        Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        ViewModel?.OpenNextVideo();
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is EndlessCatsPageViewModel viewModel)
        {
            viewModel.StopAudioPlayback();
        }

        var mediaElement = this.FindByName<MediaElement>("EndlessCatsMediaPlayer");
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