using CatApp.ViewModel.COTD;
using CommunityToolkit.Maui.Views;

namespace CatApp.View.COTD;

public partial class CatOfTheDayPage : ContentPage
{
    public CatOfTheDayPageViewModel ViewModel { get; set; }
    public CatOfTheDayPage(CatOfTheDayPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        ViewModel.StartAudioPlayback();

        var mediaElement = this.FindByName<MediaElement>("CotdMediaPlayer");
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
    }

    // Set first video from list
    public async Task SetFirstVideo(MediaElement mediaElement)
    {
        if (BindingContext is CatOfTheDayPageViewModel viewModel)
        {
            viewModel.SetFirstVideoSource(mediaElement);
        }
    }

    // Stop audio and video when page closes
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is CatOfTheDayPageViewModel viewModel)
        {
            viewModel.StopAudioPlayback();
        }

        var mediaElement = this.FindByName<MediaElement>("CotdMediaPlayer");
        if (mediaElement != null)
        {
            mediaElement.Stop();
        }
    }

    // Reopen control buttons
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        ViewModel.OnScreenTap();
        CloseControlButtons();
    }

    // Hide menu and swipe after 5 seconds
    private async void CloseControlButtons()
    {
        // Wait for 5 seconds
        await Task.Delay(5000);

        ViewModel.HideControlButtons();
    }
}