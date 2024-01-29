using CatApp.ViewModel.Menu;

namespace CatApp.View.Menu;

public partial class MenuMainPage : ContentPage
{
	public MenuMainPageViewModel ViewModel { get; set; }
    public MenuMainPage(MenuMainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}
}