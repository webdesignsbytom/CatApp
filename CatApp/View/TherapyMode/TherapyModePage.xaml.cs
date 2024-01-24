using CatApp.ViewModel.TherapyMode;

namespace CatApp.View.TherapyMode;

public partial class TherapyModePage : ContentPage
{
	public TherapyModeViewModel ViewModel { get; set; }
	public TherapyModePage(TherapyModeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}

}