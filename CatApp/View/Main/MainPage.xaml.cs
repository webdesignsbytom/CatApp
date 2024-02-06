using CatApp.ViewModel.Main;

namespace CatApp.View.Main
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel { get; set; }
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = ViewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            if (ViewModel != null && ViewModel.User.HasReviewedApp == false)
            {

                if (ViewModel.ReviewVisitCount == 3)
                {
                    ViewModel.OpenReviewModal();
                    ViewModel.ResetReviewInt();
                }
                else
                {
                    ViewModel.IncreaseReviewInt();
                }
            }
        }
    }
}
