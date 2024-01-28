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
            mediaElement.Play();
        }
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
}