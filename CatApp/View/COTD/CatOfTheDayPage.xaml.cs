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
        if (BindingContext is CatOfTheDayPageViewModel viewModel)
        {
            viewModel.StartAudioPlayback();
        }

        var mediaElement = this.FindByName<MediaElement>("CotdMediaPlayer");
        if (mediaElement != null)
        {
            mediaElement.Play();
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