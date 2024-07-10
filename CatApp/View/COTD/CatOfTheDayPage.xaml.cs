using CatApp.Services.Utils;
using CatApp.ViewModel.COTD;
using CommunityToolkit.Maui.Views;

namespace CatApp.View.COTD;

public partial class CatOfTheDayPage : ContentPage
{
    private readonly VideoService _videoService;

    public CatOfTheDayPage(VideoService videoService)
    {
        InitializeComponent();
        _videoService = videoService;
    }

    protected override async void OnAppearing()
    {
        await FetchAndDisplayVideoAsync();
    }

    private async Task FetchAndDisplayVideoAsync()
    {
        try
        {
            string videoPath = "/videos/video"; // Replace with your actual video path
            string localVideoPath = await _videoService.FetchVideoAsync(videoPath);

            // Assuming you have a VideoPlayer control in your XAML
            CotdMediaPlayer.Source = localVideoPath;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to get video", ex);
        }
    }

    // Set first video from list

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
}