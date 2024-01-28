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
            mediaElement.Play();
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
}