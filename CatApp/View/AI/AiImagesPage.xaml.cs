using CatApp.ViewModel.AI;

namespace CatApp.View.AI;

public partial class AiImagesPage : ContentPage
{
	public AiImagesPageViewModel ViewModel { get; set; }
    public AiImagesPage(AiImagesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}
}