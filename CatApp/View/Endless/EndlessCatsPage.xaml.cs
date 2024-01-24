using CatApp.ViewModel.Endless;

namespace CatApp.View.Endless;

public partial class EndlessCatsPage : ContentPage
{
	public EndlessCatsPageViewModel ViewModel { get; set; }
    public EndlessCatsPage(EndlessCatsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}
}