using CatApp.ViewModel.COTD;

namespace CatApp.View.COTD;

public partial class CatOfTheDayPage : ContentPage
{
	public CatOfTheDayPageViewModel ViewModel { get; set; }
    public CatOfTheDayPage(CatOfTheDayPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}
}