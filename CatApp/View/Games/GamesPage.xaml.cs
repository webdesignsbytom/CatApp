using CatApp.ViewModel.Games;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace CatApp.View.Games;

public partial class GamesPage : ContentPage
{
	public GamesPageViewModel ViewModel { get; set; }
	public GamesPage(GamesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        SKImageInfo info = e.Info;
        SKSurface surface = e.Surface;
        var canvas = surface.Canvas; // Assign the canvas to the property

        canvasView.IgnorePixelScaling = true;
        canvas.Clear(SKColors.Red);

        ViewModel.GameLoop(canvas);
        // Invalidate the canvas to cause a redraw
        canvasView.InvalidateSurface();
    }
}