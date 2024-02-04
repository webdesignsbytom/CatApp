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

        PostAppearanceActions();
    }

    private async void PostAppearanceActions()
    {
        // Wait for 5 seconds
        await Task.Delay(5000);

        ViewModel.RemoveSwipeModal();
        ViewModel.HideControlButtons();
    }

    public async Task SetFirstVideo(MediaElement mediaElement)
    {
        if (BindingContext is CatOfTheDayPageViewModel viewModel)
        {
            viewModel.SetFirstVideoSource(mediaElement);
        }
    }

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

}