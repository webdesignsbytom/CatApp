using Aptabase.Maui;
using CatApp.ViewModel.COTD;
using CatApp.ViewModel.Endless;
using CatApp.ViewModel.TherapyMode;

namespace CatApp
{
    public partial class App : Application
    {
        // Analytics
        IAptabaseClient _aptabase;

        // Models
        public CatOfTheDayPageViewModel CotdViewModel { get; set; }
        public EndlessCatsPageViewModel EndlessCatsViewModel { get; set; }
        public TherapyModeViewModel TherapyViewModel { get; set; }

        public App(IAptabaseClient aptabase, CatOfTheDayPageViewModel cotdViewModel, EndlessCatsPageViewModel endlessCatsViewModel, TherapyModeViewModel therapyViewModel)
        {
            BindingContext = CotdViewModel = cotdViewModel;
            BindingContext = EndlessCatsViewModel = endlessCatsViewModel;
            BindingContext = TherapyViewModel = therapyViewModel;

            InitializeComponent();

            // Analytics
            _aptabase = aptabase;

            TrackAppOpen();

            MainPage = new AppShell();
        }

        private void TrackAppOpen()
        {
            _aptabase.TrackEvent("app_started");
        }

        protected override void OnSleep()
        {
            // Stop audio and video playback here
            CotdViewModel.StopAudioPlayback();
            EndlessCatsViewModel.StopAudioPlayback();
            TherapyViewModel.StopAudioPlayback();
        }
    }
}
